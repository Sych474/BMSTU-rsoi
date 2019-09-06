if docker stack deploy  -c docker_stack.yml  svyat_stack; then
  echo -e "\n[INFO] Stack was successfully deployed."
else
    echo -e "\n[ERROR] Couldn't deploy docker stack."
  exit 4
fi