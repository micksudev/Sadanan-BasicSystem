<?php

header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Headers: X-Requested-With");

require_once __DIR__ . '/../db.php';

$db = new Database();

$api = $_SERVER["REQUEST_METHOD"];

if ($api == 'POST') {

    $username = isset($_POST['username']) ? $_POST['username'] : '';
    $password = isset($_POST['password']) ? $_POST['password'] : '';

    if (empty($username) || empty($password)) {
        echo $db->message('Username and password are required', 422);
        exit();
    }

    echo $db->userRegister($username, $password);
}