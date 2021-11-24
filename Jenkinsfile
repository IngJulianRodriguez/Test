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
                echo "Hello World1"
            }
        }
        stage('Test') {
            steps {
                echo "Hello World2"
            }
        }
        stage('Deploy') {
            steps {
                echo "Hello World3"
            }
        }
    }
}
