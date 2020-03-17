#! /bin/bash -e

fn-push() {
    local image_name=$1
    local release_type=$2
    local current_tag="$(cat ./current-tag | tr -d '\n')"
    local sem_vers=($(echo $current_tag | tr "." " "))
    local major="${sem_vers[0]}"
    local minor="${sem_vers[1]}"
    local release_tag="${major}"
    local master_tag="${major}.${minor}"
        
    if [[ "${release_type}" == "master" ]]; then
        docker push btcdocker.azurecr.io/$image_name:$current_tag
        docker tag  btcdocker.azurecr.io/$image_name:$current_tag btcdocker.azurecr.io/$image_name:latest
        docker push btcdocker.azurecr.io/$image_name:latest
        exit 0
    fi

}

fn-push "$@"