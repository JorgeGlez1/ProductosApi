-- Script de base de datos para ProductosApi (MySQL)
CREATE DATABASE IF NOT EXISTS tienda_db
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE tienda_db;

CREATE TABLE IF NOT EXISTS productos (
    id             INT AUTO_INCREMENT PRIMARY KEY,
    nombre         VARCHAR(100)   NOT NULL,
    descripcion    VARCHAR(255)   NULL,
    categoria      VARCHAR(50)    NOT NULL,
    precio         DECIMAL(10,2)  NOT NULL,
    stock          INT            NOT NULL DEFAULT 0,
    fecha_registro DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Datos de ejemplo
INSERT INTO productos (nombre, descripcion, categoria, precio, stock) VALUES
('Laptop Lenovo IdeaPad', 'Ryzen 5, 16 GB RAM, 512 GB SSD', 'Computo', 13999.00, 10),
('Mouse Logitech M170', 'Mouse inalámbrico', 'Accesorios', 249.50, 50),
('Monitor Samsung 24"', 'Full HD, 75 Hz', 'Monitores', 2899.99, 15);
