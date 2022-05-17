
TOTAL_REQS=10000
PARRALLEL_REQS=1000

DOMAIN="$(openssl rand -hex 10).com"

for i in $(seq 1 $TOTAL_REQS); do
    while [ $(jobs -r | wc -l) -ge $PARRALLEL_REQS ]; do
        sleep 0.1
    done
    (
        STATUS=$(curl -s -o /dev/null -w "%{http_code}" -X POST --header 'Content-Type: application/json' http://localhost:8000/api/v1/email --data '{"EmailAddress": "'$i'@'$DOMAIN'", "IsConsumer": true, "IsBusiness": false, "IsInvestor": false}')
        echo "test$i@$DOMAIN: $STATUS"
    ) &
done
wait
echo