<?php

header("Access-Control-Allow-Methods: POST");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Headers: X-Requested-With");

require_once __DIR__ . '/../../db.php';

$db = new Database();

$api = $_SERVER["REQUEST_METHOD"];

if ($api == 'POST') {

    $token = isset($_POST['token']) ? $_POST['token'] : '';
    $userId = isset($_POST['user_id']) ? $_POST['user_id'] : '';
    $amount = isset($_POST['amount']) ? $_POST['amount'] : '';

    if(empty($token))
    {
        echo $db->message('token are required', 422);
        exit();
    }

    if (empty($userId) || empty($amount)) {
        echo $db->message('user_id and amount are required', 422);
        exit();
    }

    echo $db->updateDiamond($token, $userId, -$amount);
}