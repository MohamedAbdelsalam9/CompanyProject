Delete From Project;
Delete From Client;
Delete From Employee;
Delete From Department;
Delete From Branch;
Delete From AppUser;
Delete From Stock;
Delete From Warehouse;
Delete From Supply;
Delete From Supplier;
Delete From Part;
Delete From Service;

INSERT INTO Department (Dnum, name) VALUES 
(1, 'Management'), 
(2, 'Sales'), 
(3, 'Operations');

INSERT INTO Branch (Bnum, name, phone, address) VALUES 
(1, 'MainBranch', '00000000', 'Cairo'), 
(2, 'AlexBranch', '00000000', 'Alexandira');

DBCC CHECKIDENT ('[Client]', RESEED, 0);
INSERT INTO Client (first_name, last_name, email, phone, fax, sex, address, company, purchases) VALUES
('Mohamed', 'Ahmed', 'mahmed@A.com', '00000000', '00000000', 'M', 'Cairo', 'A', 50),
('Omar', 'Ahmed', 'oahmed@B.com', '00000000', '00000000', 'M', 'Cairo', 'B', 30),
('Nadia', 'Mohamed', 'nmohamed@C.com', '00000000', '00000000', 'F', 'Alexandria', 'C', 60),
('Ashraf', 'Hesham', 'ahesham@D.com', '00000000', '00000000', 'M', 'Cairo', 'D', 0),
('Amr', 'Yassin', 'ayassin@E.com', '00000000', '00000000', 'M', 'Alexandira', 'E', 10),
('Ibrahim', 'Omar', 'iomar@F.com', '00000000', '00000000', 'M', 'Cairo', 'F', 50),
('Fahmy', 'Ahmed', 'fahmed@G.com', '00000000', '00000000', 'M', 'Cairo', 'G', 90),
('Menna', 'Mohamed', 'mmohamed@H.com', '00000000', '00000000', 'F', 'Cairo', 'H', 0),
('May', 'Abdalla', 'mabdalla@I.com', '00000000', '00000000', 'F', 'Portsaid', 'I', 20),
('Aya', 'Ibrahim', 'aibrahim@J.com', '00000000', '00000000', 'F', 'Cairo', 'J', 50),
('Mohamed', 'Amr', 'mamr@K.com', '00000000', '00000000', 'M', 'Cairo', 'K', 10),
('Rania', 'Abdalla', 'rabdalla@L.com', '00000000', '00000000', 'F', 'Alexandria', 'L', 40),
('Hesham', 'Ahmed', 'hahmed@M.com', '00000000', '00000000', 'M', 'Cairo', 'M', 20),
('Sobhy', 'Mohamed', 'smohamed@N.com', '00000000', '00000000', 'M', 'Cairo', 'N', 10),
('Yasser', 'Fahmy', 'yfahmy@O.com', '00000000', '00000000', 'M', 'Cairo', 'O', 60);

DBCC CHECKIDENT ('[Employee]', RESEED, 0);
INSERT INTO Employee (first_name, last_name, email, phone, position, sex, address, salary, Super_EID, E_Bnum, E_Dnum) VALUES
('Ahmed', 'Abdalla', 'aabdalla@ies.com', '00000000', 'CEO', 'M', 'Cairo', 100, null, 1, 1),
('Ahmed', 'Fahmy', 'afahmy@ies.com', '00000000', 'CFO', 'M', 'Cairo', 50, 1, 1, 1),
('Mohamed', 'Omar', 'momar@ies.com', '00000000', 'COO', 'M', 'Cairo', 50, 1, 1, 1),
('Yassin', 'Hatem', 'yhatem@ies.com', '00000000', 'BranchManager', 'M', 'Alexandria', 30, 1, 2, 1),
('Hesham', 'Sayed', 'hsayed@ies.com', '00000000', 'PM', 'M', 'Cairo', 20, 3, 1, 3),
('Basma', 'Ibrahim', 'bibrahim@ies.com', '00000000', 'associate', 'F', 'Cairo', 18, 2, 1, 1),
('Ahmed', 'Hassan', 'ahassan@ies.com', '00000000', 'associate', 'M', 'Cairo', 15, 5, 1, 3),
('Mohamed', 'Emad', 'memad@ies.com', '00000000', 'associate', 'M', 'Cairo', 13, 5, 1, 3),
('Abdalla', 'Hussein', 'ahussein@ies.com', '00000000', 'associate', 'F', 'Alexandria', 10, 4, 2, 2),
('Ibrahim', 'Mohamed', 'imohamed@ies.com', '00000000', 'associate', 'F', 'Alexandria', 12, 4, 2, 2),
('Marwa', 'Yasser', 'myasser@ies.com', '00000000', 'associate', 'F', 'Alexandria', 14, 4, 2, 2),
('Rania', 'Omar', 'romar@ies.com', '00000000', 'associate', 'F', 'Alexandra', 10, 4, 2, 3),
('Norhan', 'Mohamed', 'nmohamed@ies.com', '00000000', 'associate', 'F', 'Cairo', 13, 5, 1, 2),
('Fahmy', 'Ahmed', 'fahmed@ies.com', '00000000', 'associate', 'M', 'Cairo', 18, 5, 1, 2),
('Yasser', 'Ahmed', 'yahmed@ies.com', '00000000', 'associate', 'M', 'Cairo', 5, 5, 1, 2);

INSERT INTO AppUser (username, password, EID, CID, role) VALUES 
('1', '11111111', 1, null, 'admin'), 
('2', '11111111', 6, null, 'employee'),
('3', '11111111', null, 1, 'client'),
('4', '11111111', 2, null, 'admin'), 
('5', '11111111', 3, null, 'admin'), 
('6', '11111111', 7, null, 'employee'),
('7', '11111111', 8, null, 'employee'),
('8', '11111111', null, 2, 'client'),
('9', '11111111', null, 3, 'client'),
('10', '11111111', 4, null, 'admin'), 
('11', '11111111', 9, null, 'employee'),
('12', '11111111', null, 4, 'client'),
('13', '11111111', 10, null, 'employee'), 
('14', '11111111', 11, null, 'employee'), 
('15', '11111111', 12, null, 'employee');

INSERT INTO Part (PartID, name) VALUES 
('1', 'DrillA1'), 
('2', 'DrillA2'), 
('3', 'ScrewdriverA1'), 
('4', 'ScrewdriverA2'), 
('5', 'PowerHammerA1'), 
('6', 'PowerHammerA2'), 
('7', 'DriverKit'), 
('8', 'SecurityCamera'), 
('9', 'FireAlarm'), 
('10', 'WallInsulationA1'), 
('11', 'WallInsulationA2');

INSERT INTO Warehouse (WID, address) VALUES 
('1', 'Cairo'), 
('2', 'Alexandria'), 
('3', 'Portsaid');

INSERT INTO Stock (ST_partid, ST_WID, quantity) VALUES 
('1', '1', 2), 
('1', '2', 3), 
('1', '3', 2), 
('2', '1', 5), 
('2', '2', 3), 
('2', '3', 1), 
('3', '1', 4), 
('3', '2', 2), 
('4', '1', 5), 
('4', '3', 2), 
('5', '2', 4), 
('5', '3', 1), 
('6', '1', 2), 
('6', '2', 6), 
('6', '3', 3), 
('7', '1', 3), 
('7', '2', 2), 
('7', '3', 7), 
('8', '1', 10), 
('8', '2', 8), 
('8', '3', 3), 
('9', '1', 6), 
('9', '2', 11), 
('9', '3', 5),
('10', '1', 3), 
('10', '2', 2), 
('10', '3', 4),
('11', '1', 2), 
('11', '2', 3), 
('11', '3', 1);

INSERT INTO Supplier (SupID, name, email, phone, fax, company, address) VALUES
('1', 'Ahmed Mohamed', 'amohamed@bosch.com', '00000000', '00000000', 'Bosch', 'Cairo'),
('2', 'Samir Fayed', 'sfayed@dewalt.com', '00000000', '00000000', 'Dewalt', 'Cairo'),
('3', 'Osama Mohsen', 'omohsen@reolink.com', '00000000', '00000000', 'Reolink', 'Cairo'),
('4', 'Salma Aziz', 'saziz@samsung.com', '00000000', '00000000', 'Samsung', 'Cairo'),
('5', 'Omar Farouk', 'ofarouk@birllaid.com', '00000000', '00000000', 'Birllaid', 'Alexandria'),
('6', 'Amr Hassan', 'ahassan@lingda.com', '00000000', '00000000', 'Lingda', 'Portsaid'),
('7', 'Ehab Galal', 'egalal@blackdecker.com', '00000000', '00000000', 'Black&Decker', 'Cairo');

INSERT INTO Supply (S_partID, Sup_PID, qtm, date) VALUES 
('1', '1', 4, '2018-06-10'), 
('1', '7', 5, '2018-07-30'), 
('2', '7', 8, '2018-06-01'),
('3', '2', 5, '2018-07-12'),
('4', '7', 8, '2018-06-15'),
('4', '1', 4, '2018-08-12'),
('5', '2', 6, '2018-06-14'),
('5', '7', 4, '2018-07-06'),
('6', '1', 10, '2018-06-20'),
('6', '2', 7, '2018-07-13'),
('6', '7', 5, '2018-08-01'),
('7', '2', 10, '2018-06-25'),
('9', '4', 12, '2018-07-14'),
('10', '5', 6, '2018-06-28'),
('11', '5', 5, '2018-08-10'),
('11', '6', 8, '2018-06-24');

INSERT INTO Service (SID, name, description) VALUES 
('1', 'retail', 'selling parts to companies'),
('2', 'maintenance', 'maintaining factories and tools'),
('3', 'construction', 'construction from scratch');

INSERT INTO Project (P_EID, P_SID, P_CID, startdate, price, end_date) VALUES 
(7, 2, 14, '2013-09-12', '19', '2015-12-02'),
(5, 3, 2, '2014-04-06', '200', '2020-01-26'), 
(13, 1, 8, '2017-03-13', '55', '2019-05-14'), 
(13, 1, 4, '2014-11-12', '37', '2018-12-27'),
(14, 1, 14, '2017-12-17', '45', '2018-05-13'), 
(8, 2, 2, '2015-12-28', '43', '2017-04-11'), 
(5, 3, 6, '2013-12-19', '300', '2015-12-08'), 
(14, 1, 4, '2016-03-22', '93', '2020-04-05'),
(6, 1, 1, '2016-11-01', '84', '2019-10-15'), 
(9, 1, 5, '2016-10-03', '71', '2019-02-05'), 
(6, 1, 7, '2014-07-02', '84', '2015-08-04'), 
(12, 2, 12, '2015-04-29', '76', '2016-06-30'),
(8, 2, 6, '2016-03-03', '54', '2018-09-24'), 
(10, 1, 5, '2017-03-28', '63', '2020-05-06'), 
(14, 1, 6, '2016-07-26', '43', '2017-08-28');