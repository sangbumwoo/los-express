#! /bin/bash -e

source ./script/_common.sh

fn-build() {
    local image_name=$1
    local current_tag="$(cat ./current-tag | tr -d '\n')"
    docker build $(fn-get-docker-build-env) --tag btcdocker.azurecr.io/$image_name:$current_tag --tag btcdocker.azurecr.io/$image_name:latest .
}

fn-set-nuget-auth
fn-set-git-last-commit-sha
fn-set-current-version
fn-build "$@"