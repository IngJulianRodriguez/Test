import groovy.json.JsonSlurperClassic
if (currentBuild.getBuildCauses().toString().contains('BranchIndexingCause')) {
  print "INFO: Build skipped due to trigger being Branch Indexing"
  currentBuild.result = 'ABORTED' // optional, gives a better hint to the user that it's been skipped, rather than the default which shows it's successful
  return
}
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
        stage('Deploy') {
            steps {
                script {                   
                   echo "Deploy on branch $BRANCH_NAME"      
                }
            }
        }
    }
}
