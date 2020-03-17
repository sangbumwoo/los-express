#! /bin/bash -e

fn-set-nuget-auth() {
    if [[ "${BMWAF_NUGET_USER}" == "" ]]; then
        export BMWAF_NUGET_USER=$(vault kv get -field=user secret/common/artifactory-reader)
    fi
    if [[ "${BMWAF_NUGET_PASS}" == "" ]]; then
        export BMWAF_NUGET_PASS=$(vault kv get -field=api_token secret/common/artifactory-reader)
    fi
}


fn-set-git-last-commit-sha() {
    if [[ "${GIT_LAST_COMMIT_SHA}" == "" ]]; then
        export GIT_LAST_COMMIT_SHA=$(git log -1 --pretty=%H | cut -c 1-7)
    fi
}

fn-set-current-version() {
    if [[ "${CLUSTER_SERVICE_CURRENT_VERSION}" == "" ]]; then
        export CLUSTER_SERVICE_CURRENT_VERSION=$(cat ./current-tag | tr -d '\n')
    fi
}

fn-get-docker-build-env() {
    local env=""
    env+="--build-arg BMWAF_NUGET_USER=$BMWAF_NUGET_USER "
    env+="--build-arg BMWAF_NUGET_PASS=$BMWAF_NUGET_PASS "
    env+="--build-arg GIT_LAST_COMMIT_SHA=$GIT_LAST_COMMIT_SHA "
    env+="--build-arg CLUSTER_SERVICE_CURRENT_VERSION=$CLUSTER_SERVICE_CURRENT_VERSION "

    echo $env
}