import socket
import os
import time
import threading


class FrameReceiver(threading.Thread):

    def run(self):
        listensocket = socket.socket()
        port = 8000
        maxConnections = 5
        IP = "0.0.0.0"

        listensocket.bind(('', port))
        listensocket.listen(maxConnections)

        print("Server started at " + IP + " on port " + str(port))
        img_count = 0
        while True:
            (clientSocket, adress) = listensocket.accept()

            print("Picture transfer started", adress)
            # Izbacis ovu liniju da bi prepisivao fajl
            file = open(f"traffic_data{img_count}.jpg", "w+b")

            start = time.time()

            while True:
                data = clientSocket.recv(1024)

                if not data:
                    break

                file.write(data)

            end = time.time()
            print(
                f"Image transfer time: {round(end - start, 2)} THREAD SIGNAL HERE")
            img_count += 1
            time.sleep(1)


FrameReceiver().start()


class TrafficLigth(threading.Thread):

    def run(self):
        listensocket = socket.socket()
        Port = 8001
        maxConnections = 5
        IP = socket.gethostname()

        listensocket.bind(('', Port))
        listensocket.listen(maxConnections)

        print("Server started at " + IP + " on port " + str(Port))

        (clientSocket, adress) = listensocket.accept()

        print("New connection made", adress)

        while (True):
            try:
                clientSocket.send(b"1")
                print("start sent")
                time.sleep(12)
                clientSocket.send(b"0")
                print("stop sent")
                time.sleep(12)
            except:
                print("Connection closed")
                break


TrafficLigth().start()
