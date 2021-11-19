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
                sh 'echo "Hello World"'
            }
        }
        stage('Test') {
            steps {
                sh 'echo "Hello World"'
            }
        }
        stage('Deploy') {
            steps {
                sh 'echo "Hello World"'
            }
        }
    }
}
