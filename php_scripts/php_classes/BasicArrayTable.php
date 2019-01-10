<?php
abstract class BasicArrayTable implements ITableArray {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;
	}
	public function runSQL(string $sql, array $colorArray){
		$str = str_replace("{ARRAY}",implode(",",$colorArray),$sql);
		$res = $this->pdo->query($str);
		if(!$res){
			var_dump($str);
			var_dump($this->pdo->errorInfo());
			die();
		}
		return $res;
	}
	abstract function arrayInsert();
	abstract function elementInsert($element);
	abstract function setArrayHolder($id);
	public function runInsertQueries(array $symbols){
		$this->arrayInsert();
		$id = $this->pdo->lastInsertId();
		$this->setArrayHolder($id);
		foreach($symbols as $key => $value){
			$this->elementInsert($value);
		}
		return $id;
	}

}
