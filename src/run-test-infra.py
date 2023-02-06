import subprocess

compose_command = "docker-compose -f ./docker-compose-tests.yml -f ./docker-compose-tests.override.yml up " \
                  "postgresql-test nosql-test redis-test rabbitmq-test identity-api-test payment-api-test"

subprocess.run(compose_command, shell=True, check=True)
