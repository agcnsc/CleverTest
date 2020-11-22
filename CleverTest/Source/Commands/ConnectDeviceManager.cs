using CleverTest.Source.FFmpeg;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using static CleverTest.Source.FFmpeg.FFmpegManager;

namespace CleverTest.Source.Commands
{
    public class ConnectDeviceManager
    {
        public string display = "";
        byte[] buffer = new byte[1024 * 8];
        Socket vedioClient;
        Socket controlClient;
        NetworkStream vedioStream;
        NetworkStream controlStream;

        FFmpegManager fFmpeg = new FFmpegManager();


        public Task<StartServerResult> StartServer()
        {
            return Task.Run(() =>
            {
                var brand = CmdHelper.Run(ADBCmd.Brand, true);
                var name = CmdHelper.Run(ADBCmd.Name, true);
                var release = CmdHelper.Run(ADBCmd.Release, true);
                var sdk = CmdHelper.Run(ADBCmd.Sdk, true);

                try
                {
                    if (int.Parse(sdk) < 21)
                    {
                        return StartServerResult.NotSupportSDK;
                    }
                }
                catch
                {
                    return StartServerResult.NotConnect;
                }

                display = brand + " " + name + " Android " + release + "(" + sdk + ")";
                Console.WriteLine("display:" + display);

                SendAndStartServer();
                return StartServerResult.Success;
            });
        }

        public async void ConnectServer(ShowBitmap show)
        {
            await Task.Run(async () =>
            {
                await Task.Delay(3000);

                try
                {
                    vedioClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    controlClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress ipaddress = Dns.GetHostAddresses("localhost")[1];//IPAddress.Parse("127.0.0.1");
                    EndPoint point = new IPEndPoint(ipaddress, ADBCmd.ServerPort);
                    vedioClient.Connect(point);
                    controlClient.Connect(point);

                    vedioStream = new NetworkStream(vedioClient);
                    controlStream = new NetworkStream(controlClient);

                    fFmpeg.Init();
                    StartReceiveVedioStream(show);
                }
                catch (Exception error)
                {
                    Console.WriteLine("error: " + error.Message);
                }
            });
        }

        public void Close()
        {
            if (vedioStream != null)
            {
                vedioStream.Close();
                vedioStream = null;
            }

            if (controlStream != null)
            {
                controlStream.Close();
                controlStream = null;
            }

            if (vedioClient != null)
            {
                vedioClient.Close();
                vedioClient = null;
            }

            if (controlClient != null)
            {
                controlClient.Close();
                controlClient = null;
            }

            fFmpeg.Release();
        }

        async void SendAndStartServer()
        {
            await Task.Run(() =>
            {
                //send server
                CmdHelper.Run(string.Format(ADBCmd.SendServer, System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase), false);
                CmdHelper.Run(ADBCmd.Forward, false);
                CmdHelper.Run(ADBCmd.StartServer, false);
            });
        }

        async void StartReceiveVedioStream(ShowBitmap show)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    int size = vedioStream.Read(buffer, 0, buffer.Length);
                    Console.WriteLine("size: " + size);
                    if (size <= 0)
                    {
                        Close();
                        return;
                    }

                    if (size == 1)
                    {
                        continue;
                    }

                    fFmpeg.Decode(buffer, size, show);
                }
            });
        }

    }

    public enum StartServerResult
    {
        Success,
        NotConnect,
        NotSupportSDK
    }
}
