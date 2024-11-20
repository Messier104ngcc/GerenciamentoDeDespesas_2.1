
CREATE DATABASE SistemaWeb2;


-- Tabela Usuarios
CREATE TABLE Usuarios (
    UserId INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100) NOT NULL,
	UserName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
	Perfil INT NOT NULL,
    Senha NVARCHAR(256) NOT NULL, 
	ConfSenha NVARCHAR(256) NOT NULL,
	DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL
);

select * from Usuarios;

--drop table Usuarios;

-- Tabela Despesas
CREATE TABLE Despesas (
    Id INT PRIMARY KEY IDENTITY,
	UsuariosId INT NOT NULL,
	Despesa NVARCHAR(100) NOT NULL,
    Observacao NVARCHAR(200) NOT NULL,
    Valor DECIMAL(18, 2) NOT NULL,
    Vencimento DATE NOT NULL,
	Paga NVARCHAR(10) NULL DEFAULT 'Não Pago',
	FOREIGN KEY (UsuariosId) REFERENCES Usuarios(UserId),
);

select * from Despesas;

--drop table Despesas;


-- Tabela Contas Bancaria
CREATE TABLE Conta_Bancaria (
    Id INT PRIMARY KEY IDENTITY,
	UsuariosId INT NOT NULL,
	BancoId INT NOT NULL,
	Nome NVARCHAR(100) NOT NULL,
    Sobrenome NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) UNIQUE NOT NULL,
	Observacao NVARCHAR(200) NOT NULL,
    Saque DECIMAL(18, 2) NOT NULL,
	Deposito DECIMAL(18, 2) NOT NULL,
	DataHora DATETIME NOT NULL DEFAULT GETDATE(),
	FOREIGN KEY (UsuariosId) REFERENCES Usuarios(UserId),
);

select * from Conta_Bancaria;

--drop table Conta_Bancaria;

--  Tabela transações
CREATE TABLE Movimentacoes (
    Id INT PRIMARY KEY IDENTITY,
    TipoOperacao VARCHAR(20) NOT NULL, 
    Valor MONEY NOT NULL,
    Saldo DECIMAL(18, 2) NOT NULL,
    DataHora DATETIME NOT NULL DEFAULT GETDATE(),
    UsuariosId INT NOT NULL, 
    CONSTRAINT FK_Movimentacoes
    FOREIGN KEY (UsuariosId) 
    REFERENCES Usuarios(UserId) 
);

select * from Movimentacoes;

drop table Movimentacoes;


CREATE TABLE Bancos (
  Id INT PRIMARY KEY IDENTITY(1,1),
  Nome VARCHAR(255) NOT NULL,
  DataCadastro DATETIME DEFAULT GETDATE()
);



-- Utilização --

INSERT INTO Usuarios (Nome, UserName, Email, Perfil, Senha, ConfSenha)
VALUES 
('Lucas Silva Santos', 'l.silva', 'lucas@email.com', 1, '1996', '1996'),
('João Pedro',  'j.pedro', 'joao@email.com', 2, '1234', '1234');

ALTER TABLE Despesas
ALTER COLUMN observacao NVARCHAR(200) NULL;

select * from Despesas



INSERT INTO Despesas (Despesa, Observacao, Valor, Vencimento, UsuariosId) VALUES 
('Aluguel', 'Pagamento do aluguel mensal', 1200.00, '2024-11-30', 4),
('Energia', 'Conta de luz', 300.50, '2024-11-20', 4),
('Internet', 'Plano de internet de 200Mbps', 99.90, '2024-11-15', 4),
('Supermercado', 'Compras mensais', 800.00, '2024-11-10', 4),
('Transporte', 'Combustível para o mês', 450.00, '2024-11-25', 4),
('Academia', 'Mensalidade academia', 80.00, '2024-12-01', 4),
('Farmácia', 'Medicamentos', 150.75, '2024-11-18', 4),
('Seguro Carro', 'Seguro do veículo anual', 1500.00, '2024-11-28', 4),
('Celular', 'Plano mensal de celular', 45.00, '2024-11-22', 4),
('Entretenimento', 'Assinatura de streaming', 25.90, '2024-11-12', 4);


INSERT INTO Usuarios (Nome, UserName, Email, Perfil, Senha, ConfSenha)
VALUES 
('Lucas Silva Santos', 'l.silva', 'lucas@email.com', 1, '1996', '1996'),
('João Pedro',  'j.pedro', 'joao@email.com', 2, '1234', '1234');


UPDATE Usuarios
SET Senha = '3d1e557b540ac045b3b327994a351f08a443f9216f9b2b8d3a0f42b58671ac83
', ConfSenha = '3d1e557b540ac045b3b327994a351f08a443f9216f9b2b8d3a0f42b58671ac83
'
WHERE UserId= 1;

UPDATE Usuarios SET Email = 'messier104ngc4594@gmail.com' WHERE UserId = 1;

ALTER TABLE Despesas
ALTER COLUMN Observacao NVARCHAR(200) NULL;

DELETE FROM Despesas;
