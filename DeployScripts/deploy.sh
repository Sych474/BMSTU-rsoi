docker login -u $registry_login -p $registry_password
docker build -t webber1580/$SERVICE_NAME:$CI_PIPELINE_ID $PATH_TO_DOCKERFILE
docker push webber1580/$SERVICE_NAME:$CI_PIPELINE_ID

sudo apt-get install sshpass

if sshpass -p $DEPLOY_HOST_USER_PASSWORD\
   scp -o StrictHostKeyChecking=no ../docker_stack.yml $DEPLOY_HOST_USER@$DEPLOY_HOST_ADDRESS:~;
then
  echo "[INFO] tensor_stack.yml has been sent."
else
  echo "[ERROR] Couldnt send tensor_stack.yml."
  exit 1
fi

if sshpass -p "$DEPLOY_HOST_USER_PASSWORD"\
   ssh -o StrictHostKeyChecking=no $DEPLOY_HOST_USER@$DEPLOY_HOST_ADDRESS bash -s < deploy_to_prod.sh
then
  echo "[INFO] Connection to $DEPLOY_HOST_ADDRES was closed."
  echo OK
else
  echo "[ERROR] Couldnt connect to $DEPLOY_HOST_ADDRESS"
  exit 3
fi


