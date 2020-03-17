#!/bin/bash -ex

source ./script/_common.sh

fn-check-standard-endpoints() {
    dotnet build ./LosExpress 
    eval "dotnet run --project LosExpress &"
    #Waiting for the server to be available
    sleep 20 

    HEALTH_SELF_STATUS_CODE=$(curl -X GET 'http://localhost:5000/.internal/v1/health/self' -H 'accept: application/json' -w %{http_code})
    SWAGGER_STATUS_CODE=$(curl -X GET 'http://localhost:5000/.internal/v1/api-definition/openapi3' -H 'accept: application/json' -w %{http_code})
    GDPR_STATUS_CODE=$(curl -X DELETE 'http://localhost:5000/.internal/v1/state/user/test-validation-usid' -H 'accept: application/json' -w %{http_code})

    if [[ $HEALTH_SELF_STATUS_CODE -ne 204 ]]; then
    echo "Error communication with the internal health endpoint"
    exit 1
    fi

    if [[ $SWAGGER_STATUS_CODE -ne 200 ]]; then
    echo "Error communication with the swagger endpoint"
    exit 1
    fi

    if [[ $GDPR_STATUS_CODE -ne 200 ]] && [[ $GDPR_STATUS_CODE -ne 204 ]] && [[ $GDPR_STATUS_CODE -ne 501 ]]; then
    echo "Error communication with the GDPR endpoint"
    exit 1
    fi

    exit 0
}

fn-set-nuget-auth
fn-check-standard-endpoints