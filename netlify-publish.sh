#!/usr/bin/env bash
set -e

## install latest .net until a new netlify image is released...
pushd /tmp
wget https://dot.net/v1/dotnet-install.sh -P /tmp
chmod u+x /tmp/dotnet-install.sh
/tmp/dotnet-install.sh -c Current
dotnet new
popd

dotnet --info

sh publish.sh
