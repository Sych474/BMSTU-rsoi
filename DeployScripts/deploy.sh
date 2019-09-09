SERVICE_NAME=characters_hub
PATH_TO_DOCKERFILE=../Rsoi.Net/CharactersHub
CI_PIPELINE_ID=1

sudo apt-get install sshpass

if sshpass -p $DEPLOY_HOST_USER_PASSWORD\
   scp -o StrictHostKeyChecking=no docker-compose.yml $DEPLOY_HOST_USER@$DEPLOY_HOST_ADDRESS:~;
then
  echo "[INFO] docker-compose.yml has been sent."
else
  echo "[ERROR] Couldnt send docker-compose.yml."
  #exit 1
fi

if sshpass -p "$DEPLOY_HOST_USER_PASSWORD"\
   ssh -o StrictHostKeyChecking=no $DEPLOY_HOST_USER@$DEPLOY_HOST_ADDRESS bash -s < DeployScripts/deploy_to_prod.sh
then
  echo "[INFO] Connection to $DEPLOY_HOST_ADDRES was closed."
  #echo OK
else
  echo "[ERROR] Couldnt connect to $DEPLOY_HOST_ADDRESS"
  #exit 3
fi


