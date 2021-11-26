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
                    bat "dir"                
                }
            }
        }
        stage('Test') {
            steps {
                script {                   
                    echo "Test on branch $BRANCH_NAME"                
                }
            }
        }
        stage('Deploy') {
            steps {
                script {                   
                    echo "Deploy on branch $BRANCH_NAME"                
                }
            }
        }
    }
}
