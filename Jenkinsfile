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
                    bat "Program.bat"                
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
        stage('Merge to Master') {
            steps {
                script {                   
                    bat '''
                    Git switch master
                    Git merge $BRANCH_NAME
                    '''
                }
            }
        }
    }
}
