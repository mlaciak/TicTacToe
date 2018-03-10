#include <iostream>
#include <string>
#include <WS2tcpip.h> // sockety
#pragma comment (lib, "ws2_32.lib")
using namespace std;
void main()
{
	setlocale(LC_ALL, "pl_PL");
	//odpalenie winsock
	WSADATA wsData;
	WORD ver = MAKEWORD(2, 2);

	int wsok = WSAStartup(ver, &wsData);
	if (wsok != 0)
	{
		cerr << "nie mozna odpalic socketa! :O " << endl;
		return;
	}
	//utowrzenie socketa dla kó³ka
	SOCKET kolko = socket(AF_INET, SOCK_STREAM, 0);
	if (kolko == INVALID_SOCKET)
	{
		cerr << "nie mozna utworzyc socketa" << endl;
		return;
	}
	//utowrzenie socketa dla krzyzyka
	SOCKET krzyzyk = socket(AF_INET, SOCK_STREAM, 0);
	if (krzyzyk == INVALID_SOCKET)
	{
		cerr << "nie mozna utworzyc socketa" << endl;
		return;
	}
	//powi¹zanie socketu kó³ka z adresem ip i portem
	sockaddr_in hint;
	hint.sin_family = AF_INET;
	hint.sin_port = htons(555);
	hint.sin_addr.S_un.S_addr = INADDR_ANY;
	bind(kolko, (sockaddr*)&hint, sizeof(hint));
	//powi¹zanie socketu krzyzyka z adresem ip i portem
	sockaddr_in hint1;
	hint1.sin_family = AF_INET;
	hint1.sin_port = htons(666);
	hint1.sin_addr.S_un.S_addr = INADDR_ANY;
	bind(krzyzyk, (sockaddr*)&hint1, sizeof(hint));
	//socket ustawiony na s³uchanie
	listen(kolko, SOMAXCONN);
	listen(krzyzyk, SOMAXCONN);
	//czekanie na po³¹czenie dla kó³ka
	sockaddr_in clientKolko;
	int clientKolkoSize = sizeof(clientKolko);
	SOCKET clientKolkoSocket = accept(kolko, (sockaddr*)&clientKolko, &clientKolkoSize);
	/*if (clientSocket == INVALID_SOCKET)
	{
	cout << "niepoprawny socket" << endl;
	}*/

	char host[NI_MAXHOST]; //client's remote name
	char service[NI_MAXHOST]; //i.e port the client is connected on

	ZeroMemory(host, NI_MAXHOST); // to samo co .: memset(host, 0, NI_MAXHOST);
	ZeroMemory(service, NI_MAXSERV);
	if (getnameinfo((sockaddr*)&clientKolko, sizeof(clientKolko), host, NI_MAXHOST, service, NI_MAXSERV, 0) == 0)
	{
		cout << host << " polaczony z portem " << service << endl;
	}
	else
	{
		inet_ntop(AF_INET, &clientKolko.sin_addr, host, NI_MAXHOST);
		cout << " polaczony z portem " << ntohs(clientKolko.sin_port) << endl;
	}
	//czekanie na po³¹czenie dla krzyzyka
	sockaddr_in clientKrzyzyk;
	int clientKrzyzykSize = sizeof(clientKrzyzyk);
	SOCKET clientKrzyzykSocket = accept(krzyzyk, (sockaddr*)&clientKrzyzyk, &clientKrzyzykSize);
	/*if (clientSocket == INVALID_SOCKET)
	{
	cout << "niepoprawny socket" << endl;
	}*/

	char hostKrzyzyk[NI_MAXHOST]; //client's remote name
	char serviceKrzyzyk[NI_MAXHOST]; //i.e port the client is connected on

	ZeroMemory(hostKrzyzyk, NI_MAXHOST); // to samo co .: memset(host, 0, NI_MAXHOST);
	ZeroMemory(serviceKrzyzyk, NI_MAXSERV);
	if (getnameinfo((sockaddr*)&clientKrzyzyk, sizeof(clientKrzyzyk), hostKrzyzyk, NI_MAXHOST, serviceKrzyzyk, NI_MAXSERV, 0) == 0)
	{
		cout << host << " polaczony z portem " << service << endl;
	}
	else
	{
		inet_ntop(AF_INET, &clientKrzyzyk.sin_addr, hostKrzyzyk, NI_MAXHOST);
		cout << " polaczony z portem " << ntohs(clientKrzyzyk.sin_port) << endl;
	}
	//zamkniêcie socketa nas³uchuj¹cego
	closesocket(kolko);
	closesocket(krzyzyk);
	//echo do clienta
	char buf[11];
	while (true)
	{
		ZeroMemory(buf, 11);
		//czekanie na klienta by przes³a³ dane

		//kó³ko.:
		int bytesReceivedKolko = recv(clientKolkoSocket, buf, 11, 0);
		if (bytesReceivedKolko == SOCKET_ERROR)
		{
			cerr << "blad w odbiorze danych!" << endl;
			break;
		}
		if (bytesReceivedKolko == 0)
		{
			cout << "klient rozlaczony" << endl;
			break;
		}
		//echo do X'a
		send(clientKrzyzykSocket, buf, bytesReceivedKolko + 1, 0);
		//krzyzyk.:
		ZeroMemory(buf, 11);
		int bytesReceivedKrzyzyk = recv(clientKrzyzykSocket, buf, 11, 0);
		if (bytesReceivedKrzyzyk == SOCKET_ERROR)
		{
			cerr << "blad w odbiorze danych!" << endl;
			break;
		}
		if (bytesReceivedKrzyzyk == 0)
		{
			cout << "klient rozlaczony" << endl;
			break;
		}
		//echo do O
		send(clientKolkoSocket, buf, bytesReceivedKrzyzyk + 1, 0);


	}
	//zamykanie socketa
	closesocket(clientKolkoSocket);
	closesocket(clientKrzyzykSocket);
	//czyszczenie winsock
	WSACleanup();
}