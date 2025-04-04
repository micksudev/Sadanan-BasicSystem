<?php

require_once("config.php");
require_once("jwt.php"); 

class Database extends Config
{
    #region Interface
    public function userRegister($username, $password)
    {
        $user = $this->fetchUser($username);

        if ($user) {
            return $this->message("Username already exists", 409);
        }

        return $this->insertUser($username, $password);
    }

    public function userLogin($username, $password)
    {
        $user = $this->fetchUser($username);

        if (!$user) {
            return $this->message("Invalid username or password", 401);
        }

        if (!password_verify($password, $user['password'])) {
            return $this->message("Invalid username or password", 401);
        }

        $this->insertHistory($user['id']);

        $userData = $this->fetchUserData($user['id']);

        $payload = array(
            "user_id" => $user['id'],
            "username" => $user['username']
        ); 
        $token = Jwt::jwt_encode($payload);

        return json_encode([
            "message" => "Login successful",
            "code" => 200,
            "token" => $token,
            "user" => [
                "user_id" => $user['id'],
                "username" => $user['username']
            ],
            "user_data" => [
                "diamond" => $userData['diamond'],
                "heart" => $userData['heart']
            ]
        ]);
    }

    public function updateDiamond($token,$userId, $amount)
    {
        #jwt decode
        $decodedPayload = Jwt::jwt_decode($token);
        if(!$decodedPayload)
        {
            return $this->message("Invalid token.", 401);
        }

        if (!isset($decodedPayload['user_id']))
        {
            return $this->message("Invalid token.", 401);
        }

        if ((int)$decodedPayload['user_id'] !== (int)$userId)
        {
            return $this->message("Invalid user.", 401);
        }
        #end jwt decode

        $userData = $this->fetchUserData($userId);

        if (!$userData) {
            return $this->message("User not found", 404);
        }

        $currentDiamond = $userData['diamond'];

        $newDiamond = max(0, min(10000, $currentDiamond + $amount));

        $sql = "UPDATE user_data SET diamond = :diamond WHERE user_id = :user_id";
        $stmt = $this->conn->prepare($sql);
        $stmt->bindParam(':diamond', $newDiamond, PDO::PARAM_INT);
        $stmt->bindParam(':user_id', $userId, PDO::PARAM_INT);

        if ($stmt->execute()) {
            $userData['diamond'] = $newDiamond;

            return json_encode([
                "message" => "Update diamond successful",
                "code" => 200,
                "user" => [
                    "user_id" => $userId
                ],
                "user_data" => [
                    "diamond" => $userData['diamond'],
                    "heart" => $userData['heart']
                ]
            ]);
        } else {
            return $this->message("Failed to update diamond", 500);
        }
    }
    #endregion

    #region Insert
    private function insertUser($username, $password)
    {
        $hashedPassword = password_hash($password, PASSWORD_DEFAULT);

        $sql = "INSERT INTO users (username, password) VALUES (:username, :password)";
        $stmt = $this->conn->prepare($sql);
        $stmt->bindParam(':username', $username, PDO::PARAM_STR);
        $stmt->bindParam(':password', $hashedPassword, PDO::PARAM_STR);

        if ($stmt->execute()) {
            $userId = $this->conn->lastInsertId();
            $this->insertUserData($userId);

            return json_encode([
                "message" => "Registration successful",
                "code" => 200,
                "user" => [
                    "user_id" => $userId,
                    "username" => $username
                ]
            ]);
        } else {
            return $this->message("Error during registration", 400);
        }
    }

    private function insertUserData($userId)
    {
        $sql = "INSERT INTO user_data (user_id) VALUES (:user_id)";
        $stmt = $this->conn->prepare($sql);
        $stmt->bindParam(':user_id', $userId, PDO::PARAM_INT);
        $stmt->execute();
    }

    private function insertHistory($userId)
    {
        $sql = "INSERT INTO login_history (user_id) VALUES (:user_id)";
        $stmt = $this->conn->prepare($sql);
        $stmt->bindParam(':user_id', $userId, PDO::PARAM_INT);
        $stmt->execute();
    }
    #endregion

    #region Fetch
    private function fetchUser($username)
    {
        $sql = "SELECT * FROM users WHERE username = :username";
        $stmt = $this->conn->prepare($sql);
        $stmt->bindParam(':username', $username, PDO::PARAM_STR);
        $stmt->execute();

        return $stmt->fetch(PDO::FETCH_ASSOC);
    }

    private function fetchUserData($userId)
    {
        $sql = "SELECT diamond, heart FROM user_data WHERE user_id = :user_id";
        $stmt = $this->conn->prepare($sql);
        $stmt->bindParam(':user_id', $userId, PDO::PARAM_INT);
        $stmt->execute();

        return $stmt->fetch(PDO::FETCH_ASSOC);
    }
    #endregion
}