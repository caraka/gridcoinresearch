#!/bin/bash
# Build a 3.0 (quilt) source package from the in-tree debian/ + working tree.
#
# The orig tarball is generated from the source tree MINUS the packaging dir(s)
# (debian/ and any debian-* variant), so the quilt delta is debian/ only -> zero
# source patches. This is the CI equivalent of the old hand-run build-one.sh.
#
# Works for whichever variant's packaging is currently in debian/: the caller
# activates testnet by mangling debian-testnet/ -> debian/ first; this script
# just reads the *active* debian/changelog.
#
# Usage: build-source-package.sh [OUTDIR]   (default OUTDIR=./obs-out)
set -euo pipefail

OUTDIR="${1:-$PWD/obs-out}"

command -v dpkg-source >/dev/null || { echo "::error::dpkg-source not found (install dpkg-dev)"; exit 1; }
[ -f debian/changelog ] || { echo "::error::no debian/changelog in $PWD"; exit 1; }

SRC=$(dpkg-parsechangelog -SSource)
FULLVER=$(dpkg-parsechangelog -SVersion)   # e.g. 5.5.1.0-3
UPVER=${FULLVER%-*}                          # strip -<debian_revision> -> 5.5.1.0
PREFIX="${SRC}-${UPVER}"

WORK=$(mktemp -d)
trap 'rm -rf "$WORK"' EXIT

# Clean export of the working tree (including any uncommitted variant-mangle),
# with no .git. `git stash create` snapshots the worktree as a commit object;
# it prints nothing when the tree is clean, so fall back to HEAD.
REF=$(git stash create || true); REF=${REF:-HEAD}
git archive --format=tar --prefix="${PREFIX}/" "$REF" | tar -x -C "$WORK"

# orig = upstream source = tree minus the packaging dir(s)
tar caf "$WORK/${SRC}_${UPVER}.orig.tar.xz" \
  --exclude="${PREFIX}/debian" \
  --exclude="${PREFIX}/debian-testnet" \
  -C "$WORK" "${PREFIX}"

( cd "$WORK" && dpkg-source -b "${PREFIX}" )

mkdir -p "$OUTDIR"
mv "$WORK"/*.dsc "$WORK"/*.orig.tar.* "$WORK"/*.debian.tar.* "$OUTDIR"/
echo "Source package (${SRC} ${FULLVER}) in ${OUTDIR}:"
ls -1 "$OUTDIR"
