import groovy.json.JsonSlurperClassic

def jsonParse(def json) {
    new JsonSlurperClassic().parseText(json)
}
pipeline {
    agent {
        label 'master'
    }
    stages {
        stage('Build') {
            steps {
                script {                   
                    echo "Hello World1"                
                }
            }
        }
        stage('Test') {
            steps {
                script {                   
                    echo "Hello $BRANCH_NAME"                
                }
            }
        }
        stage('Deploy') {
            steps {
                script {                   
                    echo "Hello World3"                
                }
            }
        }
    }
}
