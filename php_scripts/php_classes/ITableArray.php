<?php
interface ITableArray {
	public function getPossibleArrays(array $symbolArray);
	public function insertArray(array $symbolArray);
	public function getAllElementsInArray(int $possibleSymbolArrayId);
}
