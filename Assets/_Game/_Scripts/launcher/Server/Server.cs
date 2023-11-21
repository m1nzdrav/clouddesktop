using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace _Game._Scripts.launcher.Server
{
    public class Server : MonoBehaviour
    {
        private void Start()
        {
            // Создадим новый сервер на порту 80
            new Server(80);
        }
        TcpListener Listener; // Объект, принимающий TCP-клиентов

        // Запуск сервера
        public Server(int Port)
        {
            // Создаем "слушателя" для указанного порта
            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start(); // Запускаем его

            // В бесконечном цикле
            // while (true)
            // {
            //     // Принимаем новых клиентов
            new Client(Listener.AcceptTcpClient());
            // }
        }

        // Остановка сервера
        ~Server()
        {
            // Если "слушатель" был создан
            if (Listener != null)
            {
                // Остановим его
                Listener.Stop();
            }
        }

       
    }
}