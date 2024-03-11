#! /bin/bash
TIME_FROM_SERVER=$(curl -s "http://192.168.0.1:8000")
if [ $? -ne 0 ]; then
    echo -e "\033[31mError fetching time from server.\033[0m"
    exit 1
fi
sudo date -s "$TIME_FROM_SERVER"

if [ $? -ne 0 ]; then
    echo -e "\033[31mError setting system time..\033[0m"
fi
echo -e "\033[38;2;46;139;87mSystem time updated successfully.\033[0m"