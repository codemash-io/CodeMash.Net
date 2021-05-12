#!/bin/bash

WORKING_DIR=$1
SRC_DIR=$WORKING_DIR/src
PKG_DIR=$WORKING_DIR/packages

mkdir -p $PKG_DIR
rm -rf $PKG_DIR/*.nupkg

nuget restore $SRC_DIR/CodeMash.Sdk.sln
nuget pack --configuration=release $SRC_DIR/CodeMash.Sdk.sln -p:Version=2.1.0 -p:NuspecFile=../../Nuget.Core/CodeMash.Core.nuspec  

# dotnet build --configuration=release $SRC_DIR/CodeMash.Sdk.sln
cp $SRC_DIR/**/bin/release/*.nupkg $PKG_DIR