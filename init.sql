CREATE DATABASE IF NOT EXISTS customers;
CREATE USER IF NOT EXISTS 'ruska'@'%' IDENTIFIED BY 'ruska_password';
GRANT ALL PRIVILEGES ON customers.* TO 'ruska'@'%';
FLUSH PRIVILEGES;