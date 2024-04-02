<?php

namespace LoanApproval;

class Loan implements \JsonSerializable
{
    private int $account_id;
    private int $approval_id;
    private int $somme;

    /**
     * @param int $account_id
     * @param int $approval_id
     * @param int $somme
     */
    public function __construct(int $account_id, int $approval_id, int $somme)
    {
        $this->account_id = $account_id;
        $this->approval_id = $approval_id;
        $this->somme = $somme;
    }

    public function getAccountId(): int
    {
        return $this->account_id;
    }

    public function getApprovalId(): int
    {
        return $this->approval_id;
    }

    public function getSomme(): int
    {
        return $this->somme;
    }

    public function jsonSerialize() : array
    {
        return get_object_vars($this);
    }
}