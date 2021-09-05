pipeline{
    agent any

    stages{
        stage("Build"){
            steps{
                echo "Build docker image"
          

                    sh """
                    docker build -t guillenmartins/aws-sqs-demo .
                    """
                
            }
            
        }
    }
    post{
        always{
            echo "========always========"
        }
        success{
            echo "========pipeline executed successfully ========"
        }
        failure{
            echo "========pipeline execution failed========"
        }
    }
}