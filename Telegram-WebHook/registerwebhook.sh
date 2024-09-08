#!/bin/bash

# Load environment variables
source .env

# Check if required variables are set
if [ -z "$TELEGRAM_BOT_SECRET" ] || [ -z "$TELEGRAM_WEBHOOK_API_ENDPOINT" ]; then
    echo "Required environment variables are not set. Check .env file."
    exit 1
fi

# Set the webhook
curl -X POST "https://api.telegram.org/bot${TELEGRAM_BOT_SECRET}/setWebhook?url=${TELEGRAM_WEBHOOK_API_ENDPOINT}"

# Get the webhook information
curl -X GET "https://api.telegram.org/bot${TELEGRAM_BOT_SECRET}/getWebhookInfo"

# Uncomment the following line to delete the webhook
# curl -X POST "https://api.telegram.org/bot${TELEGRAM_BOT_SECRET}/setWebhook?url="
