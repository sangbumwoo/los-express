import groovy.transform.Field

/* ----- Application Variables ----- */
def buildImage = 'btcdocker.azurecr.io/bmw-ci-dotnet:1.4.1'
def projectName = "LosExpress"
def meshAppName = "los-express"
def imageName = "losexpress"

/* ------- Pipeline Definition ----- */
pipeline {
    agent {
        label 'standard-tools-v2'
    }
    environment{
        VAULT_ADDR="http://vault.service.consul"
    }
    options {
        disableConcurrentBuilds()
    }

    stages {
        stage("Setup CI Environment") {
            steps {
                sh("bmwsys vault az-msi-login jenkins")
            }
        }
        stage("Checking feature branch") {
            when {
                not { branch("master") }
                not { branch("release/*") }
                not { branch("develop") }
            }
            agent {
                docker {
                    image(buildImage)
                    args("--user root")
                    label('standard-tools-v2')
                }
            }     
            stages {
                stage("Setup CI Environment") {
                    steps {
                        sh("bmwsys vault az-msi-login jenkins")
                    }
                }
                stage("Check current-tag updated") {
                    steps {
                        sh "check-tag-change ${meshAppName} master"
                    }
                }
                stage("Check CHANGELOG.md updated referencing current-tag") {
                    steps {
                        sh "check-changelog ${meshAppName} master"
                    }
                }
                stage("Check CHANGELOG.md includes ticket reference") {
                    steps {
                        sh "check-changelog-ticket"
                    }
                }
                stage("Run Unit Tests and Sonarqube") {
                    steps {
                        /* --------- Begin Sonar Analysis-------- */
                        sh "begin-sonar-scan ${projectName} latest ${projectName}.Unit.Tests/coverage.opencover.xml"

                        echo "Run unit test code coverage"
                        sh "with-nuget-auth code-coverage ${projectName}"

                        publishHTML target: [
                            reportName: "${projectName}.Unit.Tests - Test Coverage Report",
                            reportDir: "./report/",
                            reportFiles: "index.htm",
                            reportTitles: "${projectName}.Unit.Tests - Test Coverage Report",
                            keepAll: true,
                            alwaysLinkToLastBuild: false,
                            allowMissing: false
                        ]

                        sh "with-nuget-auth coverage-validation ${projectName} 100"

                        /* --------- End Sonar Analysis-------- */
                        sh "end-sonar-scan"
                    }
                }
            }     
        }

        stage("Check Standard Endpoints") {
            when {
                not { branch("master") }
                not { branch("release/*") }
                not { branch("develop") }
            }
            agent {
                docker {
                    image(buildImage)
                    args("--user root")
                    label('standard-tools-v2')
                }
            }
            steps {
                sh "bmwsys vault az-msi-login jenkins"
                sh "./script/check-standard-endpoints.sh"
            }
        }


        stage("Docker: Publish master tags") {
            when { branch("master") }
            steps {
                sh "script/docker-push.sh ${imageName} master"
            }
        }

        stage("Set pipeline data") {
            when { branch("master") }
            agent {
                docker {
                    image(buildImage)
                    args("--user root")
                    label('standard-tools-v2')
                }
            }
            steps {
                sh "set-pipeline-data ${meshAppName} ${env.BRANCH_NAME}"
            }
        }
        stage("Deploy to Mesh") {
            when { branch("master") }
            parallel {
                stage("NA DLY") {
                    steps {
                        sh "script/deploy-mesh.sh ${meshAppName} nadly"
                    }
                }
                stage("EU DLY") {
                    steps {
                        sh "script/deploy-mesh.sh ${meshAppName} eudly"
                    }
                }
                stage("NA INT") {
                    steps {
                        sh "script/deploy-mesh.sh ${meshAppName} naint"
                    }
                }
                stage("EU INT") {
                    steps {
                        sh "script/deploy-mesh.sh ${meshAppName} euint"
                    }
                }
                stage("NA DEV") {
                    steps {
                        sh "script/deploy-mesh.sh ${meshAppName} nadev"
                    }
                }
                stage("EU DEV") {
                    steps {
                        sh "script/deploy-mesh.sh ${meshAppName} eudev"
                    }
                }
                stage("NA PRD") {
                    steps {
                        sh "script/deploy-mesh.sh ${meshAppName} naprd"
                    }
                }
                stage("EU PRD") {
                    steps {
                        sh "script/deploy-mesh.sh ${meshAppName} euprd"
                    }
                }
            }
        }
    }

    post {
        always {
            sh("chmod -R 777 .")
            cleanWs()
        }
    }
}