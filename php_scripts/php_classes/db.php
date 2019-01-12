<?php
class db {
	public function __construct(){
		$host = '192.168.32.15:3307';
		$db   = 'projectC';
		$user = 'root';
		$pass = 'd1epwd1epw';

		$dsn = "mysql:host=$host;dbname=$db;charset=utf8";
		$this->pdo = new PDO($dsn, $user, $pass);
	}
	public function saveExec (PDOStatement $sth){
		$success = $sth->execute();
		if(!$success){

			var_dump($sth->errorInfo());
			$sth->debugDumpParams();
			throw new Exception("db error");
		}
	}
	public function fetchOrInsert(PDOStatement $fetch, PDOStatement $insert){
		$this->saveExec($fetch);
		$res = $fetch->fetch();
		if(!$res){
			$this->saveExec($insert);
			return $this->pdo->lastInsertId();
		}
		return $res["id"] ??$res["Id"] ?? die("no id to return");
	}
}
