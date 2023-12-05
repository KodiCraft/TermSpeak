#!/bin/bash

# https://stackoverflow.com/questions/59895/how-do-i-get-the-directory-where-a-bash-script-is-located-from-within-the-script
SOURCE=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

# This script is used to pack the project into a zip file.
pushd "${SOURCE}" || exit
dotnet build -c Release
popd || exit

# Create a temporary directory.
rm -rf /tmp/termspeak_pack
mkdir -p /tmp/termspeak_pack

# Copy the files to the temporary directory.
cp "${SOURCE}/README.md" /tmp/termspeak_pack
cp "${SOURCE}/manifest.json" /tmp/termspeak_pack
cp "${SOURCE}/icon.png" /tmp/termspeak_pack
cp "${SOURCE}/bin/Release/netstandard2.1/TermSpeak.dll" /tmp/termspeak_pack

# Create the zip file.
pushd /tmp/termspeak_pack || exit
zip -r termspeak.zip .

# Move the zip file to the current directory.
popd || exit
mv /tmp/termspeak_pack/termspeak.zip .

# Remove the temporary directory.
rm -rf /tmp/termspeak_pack