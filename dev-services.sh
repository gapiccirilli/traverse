#!/bin/zsh

set -e

ACTION=$1

start_services() {
    echo "Starting PostgreSQL..."
    brew services start postgresql@17

    echo "Starting Redis..."
    brew services start redis
}

stop_services() {
    echo "Stopping PostgreSQL..."
    brew services stop postgresql@17

    echo "Stopping Redis..."
    brew services stop redis
}

if [ -z "$ACTION" ]; then
    echo "Action argument is required [start|stop]"
    exit 1
fi

case $ACTION in
    start) start_services ;;
    stop) stop_services ;;
esac