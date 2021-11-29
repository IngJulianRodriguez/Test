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
        stage('Branch indexing: abort') {
            when {
                allOf {
                    triggeredBy cause: "BranchIndexingCause"
                    not { 
                        changeRequest() 
                    }
                }
            }
            steps {
                script {
                    echo "Branch discovered by branch indexing"
                    currentBuild.result = 'SUCCESS' 
                    error "Caught branch indexing..."
                }
            }
        }
        stage('Deploy') {
            steps {
                script {  
                  buildercauses = currentBuild.getBuildCauses().toString()
                   echo "Deploy on branch $buildercauses"      
                }
            }
        }
    }
}
