#! /bin/bash -ex

fn-deploy-mesh() {
    local mesh_app_name=$1
    local mesh_name=$2

    docker run --rm -i btcdocker.azurecr.io/bmw-fly:1.3.1 mesh-exec --mesh $mesh_name --job deploy --service $mesh_app_name
}

fn-deploy-mesh "$@"
