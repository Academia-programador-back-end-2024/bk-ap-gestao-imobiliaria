-- Criação do banco de dados
CREATE DATABASE ImobiliariaDB;
GO

-- Seleciona o banco de dados para uso
USE ImobiliariaDB;
GO


-- Tabela de Clientes
CREATE TABLE Clientes (
    ClienteId INT IDENTITY PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    CPF NVARCHAR(11) UNIQUE NOT NULL,
    Telefone NVARCHAR(20),
    Email NVARCHAR(100)
);
GO

-- Tabela de Corretores
CREATE TABLE Corretores (
    CorretorId INT IDENTITY PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    CPF NVARCHAR(11) UNIQUE NOT NULL,
    CRECI NVARCHAR(20) UNIQUE NOT NULL, -- Registro do Corretor
    Telefone NVARCHAR(20),
    Email NVARCHAR(100)
);
GO


-- Tabela de Imóveis
CREATE TABLE Imoveis (
    ImovelId INT IDENTITY PRIMARY KEY,
    Endereco NVARCHAR(255) NOT NULL,
    Tipo INT NOT NULL DEFAULT 1, -- Ex: Casa = 1, Apartamento = 2, Terreno = 3
    Area DECIMAL(10, 2) NOT NULL, -- Área em metros quadrados
    Valor DECIMAL(18, 2) NOT NULL, -- Valor de venda ou locação
    Descricao NVARCHAR(MAX), -- Descrição detalhada do imóvel
    Negocio INT NOT NULL DEFAULT 1, -- Indica se o imóvel está disponível para venda = 1/locação = 2/permuta = 3/inventario = 4	
	CorretorNegocioId INT,
	CorretorGestorId INT NOT NULL,
	ClienteDonoId INT NOT NULL,
	Disponivel BIT NOT NULL DEFAULT 1, --
	Fotos NVARCHAR(MAX),
    FOREIGN KEY (CorretorGestorId) REFERENCES Corretores(CorretorId),
    FOREIGN KEY (CorretorNegocioId) REFERENCES Corretores(CorretorId),
	FOREIGN KEY (ClienteDonoId) REFERENCES Clientes(ClienteId),
);
GO


-- Tabela de Favoritos
CREATE TABLE Favoritos (
    FavoritoId INT IDENTITY PRIMARY KEY,
    ClienteId INT NOT NULL,
    ImovelId INT NOT NULL,
    DataAdicionado DATE NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId),
    FOREIGN KEY (ImovelId) REFERENCES Imoveis(ImovelId)
);
GO

-- Tabela de Mensagens de Contato
CREATE TABLE MensagensContato (
    MensagemId INT IDENTITY PRIMARY KEY,
    ImovelId INT NOT NULL,
    ClienteId INT NOT NULL,
    CorretorId INT NOT NULL,
    Mensagem NVARCHAR(MAX) NOT NULL,
    DataEnvio DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ImovelId) REFERENCES Imoveis(ImovelId),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId),
    FOREIGN KEY (CorretorId) REFERENCES Corretores(CorretorId)
);
GO

-- Tabela de Perfis
CREATE TABLE Perfis (
    PerfilId INT IDENTITY PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL UNIQUE -- Ex: Cliente, Corretor, Administrador
);
GO

-- Inserir Perfis Padrão
INSERT INTO Perfis (Nome) VALUES ('Cliente');
INSERT INTO Perfis (Nome) VALUES ('Corretor');
INSERT INTO Perfis (Nome) VALUES ('Administrador');
GO

-- Tabela de Usuários
CREATE TABLE Usuarios (
    UsuarioId INT IDENTITY PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL, -- Email único para autenticação
    SenhaHash NVARCHAR(256) NOT NULL, -- Armazenar o hash da senha
    PerfilId INT NOT NULL, -- FK para identificar o perfil do usuário
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (PerfilId) REFERENCES Perfis(PerfilId)
);
GO


-- Adicionar colunas ClienteId e CorretorId à tabela Usuarios
ALTER TABLE Usuarios ADD ClienteId INT NULL, CorretorId INT NULL;


CREATE TABLE Contact (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) NOT NULL,
    Telefone NVARCHAR(20) NULL,
    Nome NVARCHAR(100) NOT NULL,
    Pendente BIT NOT NULL DEFAULT 1,
    CorretorId INT NULL,
    DataPedido DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Contact_Corretor FOREIGN KEY (CorretorId) REFERENCES Corretor(CorretorId)
);
