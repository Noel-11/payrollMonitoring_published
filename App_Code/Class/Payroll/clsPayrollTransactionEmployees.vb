Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollTransactionEmployees

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property transactionEmployeesId As String
    Public Property transactionHeaderId As String
    Public Property employeeId As String
    Public Property employeeName As String
    Public Property employeeHeir As String
    Public Property employeePosition As String
    Public Property salary As String
    Public Property salaryOld As String
    Public Property additionalIncome As String
    Public Property daysPresent As String
    Public Property daysWithoutPay As String
    Public Property lateHour As String
    Public Property lateMinute As String
    Public Property lateAmount As String
    Public Property otHour As String
    Public Property otMinute As String
    Public Property otAmount As String

    Public Property leaveCredit As String

    Public Property leaveAmount As String

    Public Property daysWithoutPayAmount As String
    Public Property grossAmount As String
    Public Property salaryAmount As String
    Public Property fixDeductionAmount As String
    Public Property deductionAmount As String
    Public Property diffAmount As String
    Public Property netAmount As String
    Public Property sortOrder As String
    Public Property isActive As String

    Public Property empPayrollMonth As String
    Public Property empPayrollYear As String
    Public Property empPayrollFrom As String
    Public Property empPayrollTo As String
    Public Property empPayrollPeriod As String
    Public Property meetingCnt As String
    Public Property meetingPeriod As String
    Public Property isInitial As String
    Public Property prevTransactionHeaderId As String

    Public Property empPayrollRemarks As String

    Public Property hrAccess As String

    Public Property allowancePermeet As String

#End Region

    Public Sub initialize()

        _transactionEmployeesId = ""
        _transactionHeaderId = ""
        _prevTransactionHeaderId = ""
        _employeeId = ""
        _employeeName = ""
        _employeeHeir = ""
        _employeePosition = ""
        _salary = "0.00"
        _salaryOld = "0.00"
        _additionalIncome = "0.00"
        _daysPresent = "0.00"
        _daysWithoutPay = "0.00"
        _lateHour = "0"
        _lateMinute = "0"
        _lateAmount = "0.00"
        _otHour = "0.00"
        _otMinute = "0.00"
        _otAmount = "0.00"

        _leaveCredit = "0.000"
        _leaveAmount = "0.00"
        _daysWithoutPayAmount = "0.00"
        _salaryAmount = "0.00"
        _fixDeductionAmount = "0.00"
        _deductionAmount = "0.00"
        _diffAmount = "0.00"
        _grossAmount = "0.00"
        _netAmount = "0.00"

        _empPayrollMonth = "0"
        _empPayrollYear = "0"
        _empPayrollFrom = Nothing
        _empPayrollTo = Nothing
        _empPayrollPeriod = ""

        _sortOrder = "0.00"
        _isActive = "N"
        _isInitial = "N"
        _empPayrollRemarks = ""

        _meetingCnt = "1"

        _hrAccess = "N"

        _allowancePermeet = "0.00"

    End Sub


    Public Function browsePayrollTransactionEmployeesForLog(ByVal _thisID As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT  employee_name, employee_position, salary, additional_income, days_present, days_without_pay, late_hour, late_minute, late_amount, gross_amount, net_amount FROM tbl_payroll_transaction_employees " & _
        " WHERE transaction_employees_id='" & _thisID & "' LIMIT 1"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")
    End Function


    Public Function browsePayrollList(ByVal thisHeaderID As String, ByVal _criteria As String) As DataTable

        Dim sql As String = ""
        Dim where As String = ""

        If _criteria <> "" Then

            where = "AND employee_name LIKE '" & _criteria & "%' "
        End If

        sql = "SELECT sort_order,employee_name,employee_position,salary_amount,additional_income,days_without_pay_amount,gross_amount, " & _
              "(fix_deduction_amount + deduction_amount) AS totalDeductions,net_amount FROM tbl_payroll_transaction_employees " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "' AND is_active = 'Y' " & where & _
              "ORDER BY sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function


    Public Function browsePayrollTransactionEmployeesPayrollSummary(ByVal thisHeaderID As String) As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""


        sql = "SELECT transaction_header_id, SUM(additional_income) AS additional_income, " & _
              "SUM(days_without_pay) AS days_without_pay, late_hour, late_minute, SUM(late_amount) AS late_amount, " & _
              "SUM(days_without_pay_amount) AS days_without_pay_amount, SUM(salary_amount) AS salary_amount, " & _
              "MAX(salary) AS salary, MAX(salary_old) AS salary_old, SUM(fix_deduction_amount) AS fix_deduction_amount, " & _
              "SUM(deduction_amount) AS deduction_amount, SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount, " & _
              "sort_order, DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to " & _
              "FROM tbl_payroll_transaction_employees " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "' AND is_active = 'Y'  " & _
              "GROUP BY transaction_header_id " & _
              "ORDER BY sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployeesPayroll(ByVal thisHeaderID As String, Optional _thisEmpName As String = "", Optional _thisNo As String = "") As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        If _thisEmpName <> "" Then
            sqlWhere = "AND employee_name LIKE '%" & _thisEmpName & "%' "
        End If

        If _thisNo <> "" Then
            Dim splitNo As String()
            splitNo = Split(_thisNo, ",")
            sqlWhere += "AND sort_order BETWEEN '" & splitNo(0) & "' AND '" & splitNo(1) & "' "
        End If

        'sql = "SELECT transaction_employees_id, transaction_header_id, tbl_payroll_transaction_employees.employee_id, CONCAT('<b>',(CASE WHEN employee_heir <> '' THEN CONCAT(employee_name,'/',employee_heir) ELSE employee_name END), " & _
        '      "(CASE WHEN tbl_payroll_employee_information.is_philhealth_exempt = 'Y' THEN ' (*)' ELSE '' END),'</b><br/>', tbl_payroll_transaction_employees.employee_position,'<br/>', FORMAT(salary, 2)) AS employee_name, " & _
        '      "employee_name AS empName, tbl_payroll_transaction_employees.employee_position, additional_income, days_present, days_without_pay, late_hour, late_minute, late_amount, ot_hour, " & _
        '      "ot_minute, days_without_pay_amount, salary_amount, fix_deduction_amount, deduction_amount, gross_amount, net_amount, sort_order, " & _
        '      "DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to, COALESCE(( " & _
        '      "SELECT COALESCE(cd_amount,0) " & _
        '      "FROM tbl_payroll_transaction_details " & _
        '      "WHERE transaction_employee_id = tbl_payroll_transaction_employees.transaction_employees_id AND cd_id = 'PERA' AND cd_type = 'WITHOUT PAY' " & _
        '      "LIMIT 1),0.00) AS peraWithoutPay,meeting_cnt,meeting_period,emp_payroll_remarks,salary, employee_heir,emp_payroll_period " & _
        '      "FROM tbl_payroll_transaction_employees " & _
        '      "LEFT JOIN tbl_payroll_employee_information ON tbl_payroll_transaction_employees.employee_id = tbl_payroll_employee_information.employee_id " & _
        '      "WHERE transaction_header_id = '" & thisHeaderID & "' AND tbl_payroll_transaction_employees.is_active = 'Y'  " & sqlWhere & _
        '      "ORDER BY sort_order,emp_payroll_month"

        sql = "SELECT transaction_employees_id, transaction_header_id, tbl_payroll_transaction_employees.employee_id, CONCAT('<b>',(CASE WHEN employee_heir <> '' THEN CONCAT(employee_name,'/',employee_heir) ELSE employee_name END), " & _
           "(CASE WHEN tbl_payroll_employee_information.is_philhealth_exempt = 'Y' THEN ' (*)' ELSE '' END),(CASE WHEN tbl_payroll_employee_information.is_senior_citizen = 'Y' THEN ' (S)' ELSE '' END),'</b><br/>', tbl_payroll_transaction_employees.employee_position,'<br/>', FORMAT(salary, 2)) AS employee_name, " & _
           "employee_name AS empName, tbl_payroll_transaction_employees.employee_position, additional_income, days_present, days_without_pay, late_hour, late_minute, late_amount, ot_hour, " & _
           "ot_minute, days_without_pay_amount, salary_amount, fix_deduction_amount, deduction_amount, gross_amount, net_amount, sort_order, " & _
           "DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to, " & _
           "0 AS peraWithoutPay, " & _
           "meeting_cnt,meeting_period,emp_payroll_remarks,salary, employee_heir,emp_payroll_period " & _
           "FROM tbl_payroll_transaction_employees " & _
           "LEFT JOIN tbl_payroll_employee_information ON tbl_payroll_transaction_employees.employee_id = tbl_payroll_employee_information.employee_id " & _
           "WHERE transaction_header_id = '" & thisHeaderID & "' AND tbl_payroll_transaction_employees.is_active = 'Y'  " & sqlWhere & _
           "ORDER BY sort_order,emp_payroll_month"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function


    Public Function browsePayrollTransactionEmployeesPayrollJO(ByVal thisHeaderID As String, Optional _thisEmpName As String = "", Optional _thisNo As String = "") As DataTable
        Dim sql As String = ""
        Dim sqlWhere As String = ""

        If _thisEmpName <> "" Then
            sqlWhere = "AND employee_name LIKE '%" & _thisEmpName & "%' "
        End If

        If _thisNo <> "" Then

            Dim splitNo As String()

            splitNo = Split(_thisNo, ",")

            sqlWhere += "AND sort_order BETWEEN '" & splitNo(0) & "' AND '" & splitNo(1) & "' "

        End If


        sql = "SELECT transaction_employees_id, tbl_payroll_transaction_header.transaction_header_id, tbl_payroll_transaction_employees.employee_id, CONCAT('<b>',(CASE WHEN employee_heir <> '' THEN CONCAT(employee_name,'/',employee_heir) ELSE employee_name END), " & _
          "(CASE WHEN tbl_payroll_employee_information.is_philhealth_exempt = 'Y' THEN ' (*)' ELSE '' END),(CASE WHEN tbl_payroll_employee_information.is_senior_citizen = 'Y' THEN ' (S)' ELSE '' END),'</b><br/>', tbl_payroll_transaction_employees.employee_position,'<br/>', FORMAT(salary, 2)) AS employee_name, " & _
          "employee_name AS empName, tbl_payroll_transaction_employees.employee_position, additional_income, days_present, days_without_pay, late_hour, late_minute, late_amount, ot_hour, " & _
          "ot_minute, days_without_pay_amount, salary_amount, fix_deduction_amount, deduction_amount, gross_amount, net_amount, sort_order, " & _
          "DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to, 0.00 AS peraWithoutPay,meeting_cnt,meeting_period,emp_payroll_remarks,salary, employee_heir, " & _
          "(CASE WHEN tbl_payroll_transaction_header.payroll_status <> 'PREPARATION' THEN 'FALSE' ELSE 'TRUE' END) AS isPrepare " & _
          "FROM tbl_payroll_transaction_employees " & _
          "INNER JOIN tbl_payroll_transaction_header ON tbl_payroll_transaction_employees.transaction_header_id = tbl_payroll_transaction_header.transaction_header_id " & _
          "LEFT JOIN tbl_payroll_employee_information ON tbl_payroll_transaction_employees.employee_id = tbl_payroll_employee_information.employee_id " & _
          "WHERE tbl_payroll_transaction_header.transaction_header_id = '" & thisHeaderID & "' AND tbl_payroll_transaction_employees.is_active = 'Y'  " & sqlWhere & _
          "ORDER BY sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployeesPayrollMode(ByVal _thisEmpId As String, ByVal _thisMonth As String, ByVal _thisYear As String, ByVal _thisComType As String, ByVal _thisMode As String) As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        sqlWhere = "AND employee_id = '" & _thisEmpId & "' AND emp_payroll_month = '" & _thisMonth & "' AND payroll_compensation_type = '" & _thisComType & "' AND payroll_year = '" & _thisYear & "' AND payroll_mode = '" & _thisMode & "' "

        sql = "SELECT transaction_employees_id, tbl_payroll_transaction_employees.transaction_header_id, employee_id, CONCAT('<b>',employee_name,'</b><br/>', employee_position,'<br/>', " & _
              " FORMAT(salary, 2)) AS employee_name,employee_name AS empName, employee_position, additional_income, days_present, days_without_pay, late_hour, late_minute, late_amount, ot_hour, ot_minute, days_without_pay_amount, " & _
              " salary_amount, fix_deduction_amount, deduction_amount, gross_amount, net_amount, sort_order, DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, " & _
              " DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to, " & _
              "COALESCE((SELECT COALESCE(cd_amount,0) FROM tbl_payroll_transaction_details " & _
              " WHERE transaction_employee_id = tbl_payroll_transaction_employees.transaction_employees_id " & _
              " AND cd_id = 'PERA' AND cd_type = 'WITHOUT PAY' LIMIT 1),0.00) AS peraWithoutPay FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_transaction_header ON tbl_payroll_transaction_employees.transaction_header_id = tbl_payroll_transaction_header.transaction_header_id " & _
              " WHERE  tbl_payroll_transaction_employees.is_active = 'Y'  " & sqlWhere & _
              " ORDER BY sort_order LIMIT 1"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployeesOtherPayroll(ByVal thisHeaderID As String, Optional _thisEmpName As String = "", Optional _thisNo As String = "") As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        If _thisEmpName <> "" Then

            sqlWhere = "AND employee_name LIKE '%" & _thisEmpName & "%' "

        End If

        If _thisNo <> "" Then

            Dim splitNo As String()

            splitNo = Split(_thisNo, ",")

            sqlWhere += "AND sort_order BETWEEN '" & splitNo(0) & "' AND '" & splitNo(1) & "' "

        End If

        sql = "SELECT transaction_employees_id, transaction_header_id, employee_id, CONCAT('<b>',employee_name,'</b><br/>', employee_position,'<br/>', " & _
              " FORMAT(salary, 2)) AS employee_name,employee_name AS empName, employee_position, additional_income, days_present, days_without_pay, late_hour, late_minute, late_amount, ot_hour, ot_minute, days_without_pay_amount, " & _
              " salary_amount, fix_deduction_amount, deduction_amount, gross_amount, net_amount, sort_order, DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, " & _
              " DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to, " & _
              "0.00 AS peraWithoutPay,meeting_cnt,meeting_period FROM tbl_payroll_transaction_employees " & _
              " WHERE transaction_header_id = '" & thisHeaderID & "' AND is_active = 'Y'  " & sqlWhere & _
              " ORDER BY sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployeesPayrollDiff(ByVal thisHeaderID As String, Optional _thisEmpName As String = "", Optional _thisNo As String = "") As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        If _thisEmpName <> "" Then
            sqlWhere = "AND employee_name LIKE '%" & _thisEmpName & "%' "
        End If

        If _thisNo <> "" Then

            Dim splitNo As String()

            splitNo = Split(_thisNo, ",")

            sqlWhere += "AND sort_order BETWEEN '" & splitNo(0) & "' AND '" & splitNo(1) & "' "

        End If

        sql = "SELECT transaction_employees_id, transaction_header_id, tbl_payroll_transaction_employees.employee_id, CONCAT('<b>',(CASE WHEN employee_heir <> '' THEN CONCAT(employee_name,'/',employee_heir) ELSE employee_name END), " & _
              "(CASE WHEN tbl_payroll_employee_information.is_philhealth_exempt = 'Y' THEN ' (*)' ELSE '' END),'</b><br/>', tbl_payroll_transaction_employees.employee_position,'<br/>', FORMAT(salary, 2)) AS employee_name,employee_name AS empName, " & _
              "tbl_payroll_transaction_employees.employee_position, SUM(additional_income) AS additional_income, days_present, SUM(days_without_pay) AS days_without_pay, late_hour, late_minute, " & _
              "SUM(late_amount) AS late_amount, SUM(days_without_pay_amount) AS days_without_pay_amount, SUM(salary_amount) AS salary_amount, " & _
              "MAX(salary) AS salary,MAX(salary_old) AS salary_old, " & _
              "SUM(fix_deduction_amount) AS fix_deduction_amount, SUM(deduction_amount) AS deduction_amount, " & _
              "SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount, sort_order, DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, " & _
              "DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to, emp_payroll_from AS empPayrollFrom , days_present,agency_id, DATE_FORMAT(pos_valdate,'%Y-%m-%d') AS pos_valdate,tbl_payroll_employee_information.employee_status " & _
              "FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_transaction_employees.employee_id = tbl_payroll_employee_information.employee_id " & _
              " WHERE tbl_payroll_transaction_employees.is_active = 'Y' AND transaction_header_id = '" & thisHeaderID & "' " & sqlWhere & _
              " GROUP BY transaction_header_id,employee_id ORDER BY sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployeesPayrollDiffForDelete(ByVal thisHeaderID As String, Optional _thisEmpName As String = "", Optional _thisNo As String = "") As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        If _thisEmpName <> "" Then
            sqlWhere = "AND employee_name LIKE '%" & _thisEmpName & "%' "
        End If

        If _thisNo <> "" Then

            Dim splitNo As String()

            splitNo = Split(_thisNo, ",")

            sqlWhere += "AND sort_order BETWEEN '" & splitNo(0) & "' AND '" & splitNo(1) & "' "

        End If

        sql = "SELECT transaction_employees_id, transaction_header_id, tbl_payroll_transaction_employees.employee_id, CONCAT('<b>',(CASE WHEN employee_heir <> '' THEN CONCAT(employee_name,'/',employee_heir) ELSE employee_name END), " & _
              "(CASE WHEN tbl_payroll_employee_information.is_philhealth_exempt = 'Y' THEN ' (*)' ELSE '' END),'</b><br/>', tbl_payroll_transaction_employees.employee_position,'<br/>', FORMAT(salary, 2)) AS employee_name,employee_name AS empName, " & _
              "tbl_payroll_transaction_employees.employee_position, SUM(additional_income) AS additional_income, days_present, SUM(days_without_pay) AS days_without_pay, late_hour, late_minute, " & _
              "SUM(late_amount) AS late_amount, SUM(days_without_pay_amount) AS days_without_pay_amount, SUM(salary_amount) AS salary_amount, " & _
              "MAX(salary) AS salary,MAX(salary_old) AS salary_old, " & _
              "SUM(fix_deduction_amount) AS fix_deduction_amount, SUM(deduction_amount) AS deduction_amount, " & _
              "SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount, sort_order, DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, " & _
              "DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to, emp_payroll_from AS empPayrollFrom , days_present,agency_id, DATE_FORMAT(pos_valdate,'%Y-%m-%d') AS pos_valdate,tbl_payroll_employee_information.employee_status " & _
              "FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_transaction_employees.employee_id = tbl_payroll_employee_information.employee_id " & _
              " WHERE transaction_header_id = '" & thisHeaderID & "' " & sqlWhere & _
              " GROUP BY transaction_header_id,employee_id ORDER BY sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function


    Public Function browsePayrollTransactionEmployeesPayrollWithdrawal(ByVal thisHeaderID As String, Optional _thisEmpName As String = "", Optional _thisNo As String = "") As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        If _thisEmpName <> "" Then
            sqlWhere = "AND employee_name LIKE '%" & _thisEmpName & "%' "
        End If

        If _thisNo <> "" Then

            Dim splitNo As String()

            splitNo = Split(_thisNo, ",")

            sqlWhere += "AND sort_order BETWEEN '" & splitNo(0) & "' AND '" & splitNo(1) & "' "

        End If

        sql = "SELECT transaction_employees_id, transaction_header_id, tbl_payroll_transaction_employees.employee_id, CONCAT('<b>',(CASE WHEN employee_heir <> '' THEN CONCAT(employee_name,'/',employee_heir) ELSE employee_name END), " & _
              "(CASE WHEN tbl_payroll_employee_information.is_philhealth_exempt = 'Y' THEN ' (*)' ELSE '' END),' (',tbl_payroll_ref_department.deptcode,')','</b><br/>', tbl_payroll_transaction_employees.employee_position,'<br/>', FORMAT(salary, 2)) AS employee_name,employee_name AS empName, " & _
              "tbl_payroll_transaction_employees.employee_position, SUM(additional_income) AS additional_income, days_present, SUM(days_without_pay) AS days_without_pay, late_hour, late_minute, " & _
              "SUM(late_amount) AS late_amount, SUM(days_without_pay_amount) AS days_without_pay_amount, SUM(salary_amount) AS salary_amount, " & _
              "MAX(salary) AS salary,MAX(salary_old) AS salary_old, " & _
              "SUM(fix_deduction_amount) AS fix_deduction_amount, SUM(deduction_amount) AS deduction_amount, " & _
              "SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount, sort_order, DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, " & _
              "DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to, emp_payroll_from AS empPayrollFrom , days_present,agency_id " & _
              "FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_transaction_employees.employee_id = tbl_payroll_employee_information.employee_id " & _
              "INNER JOIN tbl_payroll_ref_department ON tbl_payroll_employee_information.department = tbl_payroll_ref_department.department_id " & _
              " WHERE tbl_payroll_transaction_employees.is_active = 'Y' AND transaction_header_id = '" & thisHeaderID & "' " & sqlWhere & _
              " GROUP BY transaction_header_id,employee_id ORDER BY sort_order"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")
    End Function

    Public Function browsePayrollTransactionEmployeesPayrollLedger(ByVal _thisPayroll As String, ByVal _thisEmpId As String) As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        sql = "SELECT transaction_employees_id, tbl_payroll_ref_department.deptcode AS department, tbl_payroll_transaction_employees.transaction_header_id, employee_id,employee_name AS empName, employee_position,payroll_compensation_type,payroll_compensation, " & _
              "SUM(additional_income) AS additional_income, days_present, SUM(days_without_pay) AS days_without_pay, late_hour, late_minute, SUM(late_amount) AS late_amount,payroll_classification, " & _
              "SUM(days_without_pay_amount) AS days_without_pay_amount, SUM(salary_amount) AS salary_amount, MAX(salary) AS salary, MAX(salary_old) AS salary_old,payroll_details,payroll_mode, " & _
              "SUM(fix_deduction_amount) AS fix_deduction_amount,(SUM(fix_deduction_amount) + SUM(deduction_amount)) AS deduction_amount, SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount, sort_order, " & _
              "DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to,tbl_payroll_transaction_header.payroll_period, " & _
              "tbl_payroll_transaction_header.reference_code,DATE_FORMAT(tbl_payroll_transaction_header.audited_date,'%m/%d/%Y %h:%i %p') AS audited_date FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_transaction_header ON tbl_payroll_transaction_employees.transaction_header_id = tbl_payroll_transaction_header.transaction_header_id AND " & _
              "tbl_payroll_transaction_header.transaction_header_id = '" & _thisPayroll & "' " & _
              "INNER JOIN tbl_payroll_ref_department ON tbl_payroll_transaction_header.department = tbl_payroll_ref_department.department_id " & _
              "WHERE employee_id = '" & _thisEmpId & "'  AND tbl_payroll_transaction_employees.is_active = 'Y' " & _
              "GROUP BY tbl_payroll_transaction_employees.transaction_header_id,tbl_payroll_transaction_employees.employee_id " & _
              "ORDER BY payroll_from DESC LIMIT 20"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployeesPayrollLedgerHR(ByVal _thisEmpId As String) As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        'If _thisDateChecck Then
        '    If _thisPeriodFrom <> "" And _thisPeriodTo <> "" Then

        '        sqlWhere += "AND payroll_from >= '" & _thisPeriodFrom & "' AND payroll_to <= '" & _thisPeriodTo & "'"

        '    End If
        'End If

        sql = "SELECT transaction_employees_id, tbl_payroll_transaction_employees.transaction_header_id, employee_id,employee_name AS empName, employee_position,payroll_compensation, " & _
              "SUM(additional_income) AS additional_income, days_present, SUM(days_without_pay) AS days_without_pay, late_hour, late_minute, SUM(late_amount) AS late_amount,payroll_classification, " & _
              "SUM(days_without_pay_amount) AS days_without_pay_amount, SUM(salary_amount) AS salary_amount, MAX(salary) AS salary, MAX(salary_old) AS salary_old,payroll_details,payroll_mode, " & _
              "SUM(fix_deduction_amount) AS fix_deduction_amount,(SUM(fix_deduction_amount) + SUM(deduction_amount)) AS deduction_amount, SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount, sort_order, " & _
              "DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to,tbl_payroll_transaction_header.payroll_period, " & _
              "tbl_payroll_transaction_header.reference_code,tbl_payroll_transaction_header.payroll_status FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_transaction_header ON tbl_payroll_transaction_employees.transaction_header_id = tbl_payroll_transaction_header.transaction_header_id " & _
              "WHERE employee_id = '" & _thisEmpId & "' AND tbl_payroll_transaction_header.payroll_status <> 'PREPARATION' " & sqlWhere & _
              "GROUP BY tbl_payroll_transaction_employees.transaction_header_id,tbl_payroll_transaction_employees.employee_id " & _
              "ORDER BY payroll_from DESC LIMIT 5"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")
    End Function


    Public Function browsePayrollTransactionEmployeesPayrollLedgerWithLAuditDateTimesHR(ByVal _thisEmpId As String) As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        'If _thisDateChecck Then
        '    If _thisPeriodFrom <> "" And _thisPeriodTo <> "" Then

        '        sqlWhere += "AND payroll_from >= '" & _thisPeriodFrom & "' AND payroll_to <= '" & _thisPeriodTo & "'"

        '    End If
        'End If

        sql = "SELECT transaction_employees_id, tbl_payroll_transaction_employees.transaction_header_id, employee_id,employee_name AS empName, employee_position,payroll_compensation, " & _
              "SUM(additional_income) AS additional_income, days_present, SUM(days_without_pay) AS days_without_pay, late_hour, late_minute, SUM(late_amount) AS late_amount,payroll_classification, " & _
              "SUM(days_without_pay_amount) AS days_without_pay_amount, SUM(salary_amount) AS salary_amount, MAX(salary) AS salary, MAX(salary_old) AS salary_old,payroll_details,payroll_mode, " & _
              "SUM(fix_deduction_amount) AS fix_deduction_amount,(SUM(fix_deduction_amount) + SUM(deduction_amount)) AS deduction_amount, SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount, sort_order, " & _
              "DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to,tbl_payroll_transaction_header.payroll_period, " & _
              "tbl_payroll_transaction_header.reference_code,(CASE WHEN tbl_payroll_transaction_header.payroll_status = 'POSTED' THEN 'PRE-AUDITED' WHEN tbl_payroll_transaction_header.payroll_status = 'BOOKED' THEN 'BOOKING REQUEST' ELSE tbl_payroll_transaction_header.payroll_status END) AS payroll_status, DATE_FORMAT(MAX(tbl_audit_trail.trans_datetime),'%m/%d/%Y %I:%i %p') AS trans_datetime FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_transaction_header ON tbl_payroll_transaction_employees.transaction_header_id = tbl_payroll_transaction_header.transaction_header_id " & _
              "INNER JOIN tbl_audit_trail ON tbl_payroll_transaction_header.transaction_header_id = tbl_audit_trail.link_id " & _
              "WHERE employee_id = '" & _thisEmpId & "' AND tbl_payroll_transaction_header.is_active = 'Y' " & _
              "GROUP BY tbl_payroll_transaction_employees.transaction_header_id,tbl_payroll_transaction_employees.employee_id " & _
              "ORDER BY payroll_from DESC, trans_datetime DESC LIMIT 5"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployeesPayrollLedgerAgency(ByVal _thisEmpId As String, _thisDateChecck As Boolean, _thisPeriodFrom As String, _thisPeriodTo As String, ByVal _thisAgency As String) As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        If _thisDateChecck Then
            If _thisPeriodFrom <> "" And _thisPeriodTo <> "" Then

                sqlWhere += "AND payroll_from >= '" & _thisPeriodFrom & "' AND payroll_to <= '" & _thisPeriodTo & "'"

            End If
        End If

        sql = "SELECT transaction_employees_id, tbl_payroll_transaction_employees.transaction_header_id, tbl_payroll_transaction_employees.employee_id,employee_name AS empName, employee_position,payroll_compensation_type, " & _
              "SUM(additional_income) AS additional_income, days_present, SUM(days_without_pay) AS days_without_pay, late_hour, late_minute, SUM(late_amount) AS late_amount,payroll_classification, " & _
              "SUM(days_without_pay_amount) AS days_without_pay_amount, SUM(salary_amount) AS salary_amount, MAX(salary) AS salary, MAX(salary_old) AS salary_old,payroll_details,payroll_mode, " & _
              "SUM(fix_deduction_amount) AS fix_deduction_amount,(SUM(fix_deduction_amount) + SUM(deduction_amount)) AS deduction_amount, gross_amount AS gross_amount, net_amount AS net_amount, tbl_payroll_transaction_employees.sort_order, " & _
              "DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to,tbl_payroll_transaction_header.payroll_period, " & _
              "tbl_payroll_transaction_header.reference_code, " & _
              "(CASE WHEN COALESCE(tbl_payroll_transaction_employees.hr_access,'') <> 'Y' THEN 'FALSE' ELSE 'TRUE' END) AS activeVisible,  " & _
              "(CASE WHEN COALESCE(tbl_payroll_transaction_employees.hr_access,'') <> 'Y' THEN 'TRUE' ELSE 'FALSE' END) AS inActiveVisible FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_transaction_header ON tbl_payroll_transaction_employees.transaction_header_id = tbl_payroll_transaction_header.transaction_header_id " & _
              "INNER JOIN tbl_payroll_transaction_details ON tbl_payroll_transaction_employees.transaction_employees_id = tbl_payroll_transaction_details.transaction_employee_id AND " & _
              "tbl_payroll_transaction_details.cd_agency = '" & _thisAgency & "'" & _
              "WHERE tbl_payroll_transaction_employees.employee_id = '" & _thisEmpId & "' AND tbl_payroll_transaction_header.payroll_status <> 'PREPARATION' " & sqlWhere & _
              "GROUP BY tbl_payroll_transaction_employees.transaction_header_id,tbl_payroll_transaction_employees.employee_id " & _
              "ORDER BY payroll_from DESC LIMIT 15"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployeesPayrollLedgerAgencyHR(ByVal _thisEmpId As String, _thisDateChecck As Boolean, _thisPeriodFrom As String, _thisPeriodTo As String, ByVal _thisAgency As String) As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        If _thisDateChecck Then
            If _thisPeriodFrom <> "" And _thisPeriodTo <> "" Then
                sqlWhere += "AND payroll_from >= '" & _thisPeriodFrom & "' AND payroll_to <= '" & _thisPeriodTo & "'"
            End If
        End If

        sql = "SELECT transaction_employees_id, tbl_payroll_transaction_employees.transaction_header_id, tbl_payroll_transaction_employees.employee_id,employee_name AS empName, employee_position,payroll_compensation_type, " & _
              "SUM(additional_income) AS additional_income, days_present, SUM(days_without_pay) AS days_without_pay, late_hour, late_minute, SUM(late_amount) AS late_amount,payroll_classification, " & _
              "SUM(days_without_pay_amount) AS days_without_pay_amount, SUM(salary_amount) AS salary_amount, MAX(salary) AS salary, MAX(salary_old) AS salary_old,payroll_details,payroll_mode, " & _
              "SUM(fix_deduction_amount) AS fix_deduction_amount,(SUM(fix_deduction_amount) + SUM(deduction_amount)) AS deduction_amount, gross_amount AS gross_amount, net_amount AS net_amount, tbl_payroll_transaction_employees.sort_order, " & _
              "DATE_FORMAT(emp_payroll_from,'%M %d') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to,tbl_payroll_transaction_header.payroll_period, " & _
              "tbl_payroll_transaction_header.reference_code FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_transaction_header ON tbl_payroll_transaction_employees.transaction_header_id = tbl_payroll_transaction_header.transaction_header_id " & _
              "INNER JOIN tbl_payroll_transaction_details ON tbl_payroll_transaction_employees.transaction_employees_id = tbl_payroll_transaction_details.transaction_employee_id AND " & _
              "tbl_payroll_transaction_details.cd_agency = '" & _thisAgency & "'" & _
              "WHERE tbl_payroll_transaction_employees.employee_id = '" & _thisEmpId & "' AND tbl_payroll_transaction_header.payroll_status <> 'PREPARATION' AND tbl_payroll_transaction_employees.hr_access = 'Y' " & sqlWhere & _
              "GROUP BY tbl_payroll_transaction_employees.transaction_header_id,tbl_payroll_transaction_employees.employee_id " & _
              "ORDER BY payroll_from DESC LIMIT 15"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployees(ByVal thisHeaderID As String, Optional _infoType As String = "OFFICE") As DataTable
        Dim sql As String = ""

        Dim tblInfo As String = "tbl_payroll_employee_information"

        If _infoType = "SECTOR" Then
            tblInfo = "tbl_payroll_employee_information_sector"
        End If

        sql = "SELECT transaction_employees_id, transaction_header_id, tbl_payroll_transaction_employees.employee_id, employee_name, " & _
              "tbl_payroll_transaction_employees.employee_position, salary, additional_income, " & _
              "days_present, days_without_pay, late_hour, late_minute, late_amount, days_without_pay_amount, salary_amount, " & _
              "deduction_amount, gross_amount, net_amount," & tblInfo & ".agency_id, " & tblInfo & ".employee_status FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN " & tblInfo & " ON tbl_payroll_transaction_employees.employee_id = " & tblInfo & ".employee_id " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "'  ORDER BY sort_order "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")
    End Function

    Public Function browsePayrollTransactionEmployeeSectors(ByVal thisHeaderID As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT transaction_employees_id, transaction_header_id, tbl_payroll_transaction_employees.employee_id, employee_name, " & _
              "tbl_payroll_transaction_employees.employee_position, salary, additional_income, " & _
              "days_present, days_without_pay, late_hour, late_minute, late_amount, days_without_pay_amount, salary_amount, " & _
              "deduction_amount, gross_amount, net_amount,tbl_payroll_employee_information_sector.agency_id, tbl_payroll_employee_information_sector.employee_status FROM tbl_payroll_transaction_employees " & _
              "INNER JOIN tbl_payroll_employee_information_sector ON tbl_payroll_transaction_employees.employee_id = tbl_payroll_employee_information_sector.employee_id " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "'  ORDER BY sort_order "
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")
    End Function

    Public Function browsePayrollTransactionEmployeesByEmployee(ByVal thisHeaderID As String, thisTransEmployee As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT transaction_employees_id, transaction_header_id, employee_id, employee_name, employee_position, salary, salary_old, additional_income, days_present, " & _
              "days_without_pay, late_hour, late_minute ,late_amount, ot_hour, ot_minute ,days_without_pay_amount ,salary_amount ,fix_deduction_amount ,deduction_amount,allowance_permeet, " & _
              "gross_amount, net_amount, DATE_FORMAT(emp_payroll_from,'%m/%d/%Y') AS emp_payroll_from, DATE_FORMAT(emp_payroll_to,'%m/%d/%Y') AS emp_payroll_to,days_present,emp_payroll_year,emp_payroll_month FROM tbl_payroll_transaction_employees " & _
              " WHERE transaction_header_id = '" & thisHeaderID & "' AND transaction_employees_id='" & thisTransEmployee & "'   ORDER BY sort_order LIMIT 1"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")
    End Function

    Public Function browsePayrollTransactionEmployeesOrderMultipleWithdrawal(ByVal thisHeaderID As String) As DataTable

        Dim sql As String = ""

        sql = "SELECT transaction_employees_id, transaction_header_id,tbl_payroll_transaction_employees.employee_id, sort_order FROM tbl_payroll_transaction_employees " & _
             "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_transaction_employees.employee_id = tbl_payroll_employee_information.employee_id " & _
             "INNER JOIN tbl_payroll_ref_department ON tbl_payroll_employee_information.department = tbl_payroll_ref_department.department_id " & _
             "WHERE transaction_header_id = '" & thisHeaderID & "' AND tbl_payroll_transaction_employees.is_active = 'Y'  ORDER BY department_name,last_name,first_name,middle_name "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Function browsePayrollTransactionEmployeesOrder(ByVal thisHeaderID As String, Optional _thisTblInfo As String = "tbl_payroll_employee_information") As DataTable
        Dim sql As String = ""


        'sql = "SELECT transaction_employees_id, transaction_header_id,tbl_payroll_transaction_employees.employee_id, sort_order FROM tbl_payroll_transaction_employees " & _
        '     "INNER JOIN " & _thisTblInfo & " ON tbl_payroll_transaction_employees.employee_id = " & _thisTblInfo & ".employee_id " & _
        '     "WHERE transaction_header_id = '" & thisHeaderID & "' AND tbl_payroll_transaction_employees.is_active = 'Y'  ORDER BY tbl_payroll_transaction_employees.employee_name "

        sql = "SELECT transaction_employees_id, transaction_header_id,tbl_payroll_transaction_employees.employee_id, sort_order FROM tbl_payroll_transaction_employees " & _
              "WHERE transaction_header_id = '" & thisHeaderID & "' AND tbl_payroll_transaction_employees.is_active = 'Y'  ORDER BY tbl_payroll_transaction_employees.employee_name "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")
    End Function

    Public Function browsePayrollTransactionEmployeesPeriod(ByVal thisHeaderID As String, _thisEmpId As String, thisInitial As String) As DataTable
        Dim sql As String = ""

        Dim sqlWhere As String = ""

        If thisInitial <> "" Then
            sqlWhere += "AND is_initial = '" & thisInitial & "' "
        End If

        sql = " SELECT transaction_employees_id, transaction_header_id, employee_id, DATE_FORMAT(emp_payroll_from,'%M %d, %Y') AS emp_payroll_from, emp_payroll_from AS empPayrollFrom,deduction_amount, net_amount,emp_payroll_period, " & _
              " DATE_FORMAT(emp_payroll_to,'%M %d, %Y') AS emp_payroll_to, salary_amount, gross_amount,days_without_pay_amount,late_amount,additional_income,fix_deduction_amount,employee_name AS empName,emp_payroll_remarks, " & _
              " COALESCE(salary,0.00) AS salary, COALESCE(salary_old,0.00) AS salary_old,days_without_pay,late_hour,late_minute,ot_hour,ot_minute,is_initial,days_present,meeting_cnt,meeting_period,diff_amount FROM tbl_payroll_transaction_employees " & _
              " WHERE transaction_header_id = '" & thisHeaderID & "' AND employee_id = '" & _thisEmpId & "' " & sqlWhere & _
              " ORDER BY tbl_payroll_transaction_employees.emp_payroll_from ASC"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")
    End Function

    Public Function browsePayrollTransactionEmployeesHRPeriod(_thisEmpId As String) As DataTable
        Dim sql As String = ""

        'Dim sqlWhere As String = ""

        'If thisInitial <> "" Then
        '    sqlWhere += "AND is_initial = '" & thisInitial & "' "
        'End If

        sql = " SELECT transaction_employees_id, transaction_header_id, employee_id, DATE_FORMAT(emp_payroll_from,'%M %d, %Y') AS emp_payroll_from, emp_payroll_from AS empPayrollFrom,deduction_amount, net_amount,emp_payroll_period, " & _
              " DATE_FORMAT(emp_payroll_to,'%M %d, %Y') AS emp_payroll_to, salary_amount, gross_amount,days_without_pay_amount,late_amount,additional_income,fix_deduction_amount,employee_name AS empName,emp_payroll_remarks, " & _
              " COALESCE(salary,0.00) AS salary, COALESCE(salary_old,0.00) AS salary_old,days_without_pay,late_hour,late_minute,ot_hour,ot_minute,is_initial,days_present,meeting_cnt,meeting_period,net_amount FROM tbl_payroll_transaction_employees " & _
              " WHERE employee_id = '" & _thisEmpId & "' AND hr_access = 'Y' " & _
              " ORDER BY tbl_payroll_transaction_employees.emp_payroll_from DESC"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_employees")

    End Function

    Public Sub savePayrollTransactionEmployees()

        Dim _clsUtil As New clsUtility
        Dim i As Integer = _clsUtil.stringToInteger(_clsDB.Get_DB_Item("SELECT count(*) FROM tbl_payroll_transaction_employees WHERE transaction_header_id='" & _transactionHeaderId & "'"))

        With _clsDB.dbUtility
            .fieldItems = "transaction_employees_id,transaction_header_id,employee_id,employee_name,employee_position,salary,salary_old,additional_income,days_present,days_without_pay,late_hour,late_minute,diff_amount,gross_amount,net_amount,sort_order"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "INSERT")
            _transactionEmployeesId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .ADDPARAM_CMD_String("employee_name", _employeeName)
            .ADDPARAM_CMD_String("employee_position", _employeePosition)
            .ADDPARAM_CMD_String("salary", _salary)
            .ADDPARAM_CMD_String("salary_old", _salaryOld)
            .ADDPARAM_CMD_String("additional_income", _additionalIncome)
            .ADDPARAM_CMD_String("days_present", _daysPresent)
            .ADDPARAM_CMD_String("days_without_pay", _daysWithoutPay)
            .ADDPARAM_CMD_String("late_hour", _lateHour)
            .ADDPARAM_CMD_String("late_minute", _lateMinute)
            .ADDPARAM_CMD_String("diff_amount", _diffAmount)
            .ADDPARAM_CMD_String("gross_amount", _grossAmount)
            .ADDPARAM_CMD_String("net_amount", _netAmount)
            .ADDPARAM_CMD_String("sort_order", i + 1)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollTransactionEmployeesIsInitial()
        With _clsDB.dbUtility
            .fieldItems = "additional_income,days_present,days_without_pay,late_hour,late_minute,ot_hour,ot_minute,gross_amount,net_amount,emp_payroll_month,emp_payroll_from,emp_payroll_to,emp_payroll_period,is_initial"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("additional_income", _additionalIncome)
            .ADDPARAM_CMD_String("days_present", _daysPresent)
            .ADDPARAM_CMD_String("days_without_pay", _daysWithoutPay)
            .ADDPARAM_CMD_String("late_hour", _lateHour)
            .ADDPARAM_CMD_String("late_minute", _lateMinute)
            .ADDPARAM_CMD_String("ot_hour", _otHour)
            .ADDPARAM_CMD_String("ot_minute", _otMinute)
            .ADDPARAM_CMD_String("gross_amount", _grossAmount)
            .ADDPARAM_CMD_String("net_amount", _netAmount)
            .ADDPARAM_CMD_String("emp_payroll_month", _empPayrollMonth)
            .ADDPARAM_CMD_String("emp_payroll_from", _empPayrollFrom)
            .ADDPARAM_CMD_String("emp_payroll_to", _empPayrollTo)
            .ADDPARAM_CMD_String("emp_payroll_period", _empPayrollPeriod)
            .ADDPARAM_CMD_String("is_initial", _isInitial)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub savePayrollTransactionEmployeesPeriod()

        Dim _clsUtil As New clsUtility
        Dim i As Integer = _clsUtil.stringToInteger(_clsDB.Get_DB_Item("SELECT count(*) FROM tbl_payroll_transaction_employees WHERE transaction_header_id='" & _transactionHeaderId & "'"))

        With _clsDB.dbUtility
            .fieldItems = "transaction_employees_id,transaction_header_id,employee_id,employee_name,employee_position,salary,salary_old,additional_income,days_present,days_without_pay,late_hour,late_minute,gross_amount,net_amount,emp_payroll_month,emp_payroll_year,emp_payroll_from,emp_payroll_to,emp_payroll_period,sort_order,meeting_cnt"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "INSERT")
            _transactionEmployeesId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .ADDPARAM_CMD_String("employee_name", _employeeName)
            .ADDPARAM_CMD_String("employee_position", _employeePosition)
            .ADDPARAM_CMD_String("salary", _salary)
            .ADDPARAM_CMD_String("salary_old", _salaryOld)
            .ADDPARAM_CMD_String("additional_income", _additionalIncome)
            .ADDPARAM_CMD_String("days_present", _daysPresent)
            .ADDPARAM_CMD_String("days_without_pay", _daysWithoutPay)
            .ADDPARAM_CMD_String("late_hour", _lateHour)
            .ADDPARAM_CMD_String("late_minute", _lateMinute)
            .ADDPARAM_CMD_String("gross_amount", _grossAmount)
            .ADDPARAM_CMD_String("net_amount", _netAmount)
            .ADDPARAM_CMD_String("emp_payroll_month", _empPayrollMonth)
            .ADDPARAM_CMD_String("emp_payroll_year", _empPayrollYear)
            .ADDPARAM_CMD_String("emp_payroll_from", _empPayrollFrom)
            .ADDPARAM_CMD_String("emp_payroll_to", _empPayrollTo)
            .ADDPARAM_CMD_String("emp_payroll_period", _empPayrollPeriod)
            .ADDPARAM_CMD_String("sort_order", i + 1)
            .ADDPARAM_CMD_String("meeting_cnt", _meetingCnt)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    'RENDERED SERVICE
    Public Sub savePayrollTransactionEmployeesJOES()

        Dim _clsUtil As New clsUtility
        Dim i As Integer = _clsUtil.stringToInteger(_clsDB.Get_DB_Item("SELECT count(*) FROM tbl_payroll_transaction_employees WHERE transaction_header_id='" & _transactionHeaderId & "'"))

        With _clsDB.dbUtility
            .fieldItems = "transaction_employees_id,transaction_header_id,employee_id,employee_name,employee_position,salary,salary_old,additional_income,days_present,days_without_pay,late_hour,late_minute,ot_hour,ot_minute,gross_amount,net_amount,emp_payroll_month,emp_payroll_year,emp_payroll_from,emp_payroll_to,emp_payroll_period,sort_order"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "INSERT")
            _transactionEmployeesId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .ADDPARAM_CMD_String("employee_name", _employeeName)
            .ADDPARAM_CMD_String("employee_position", _employeePosition)
            .ADDPARAM_CMD_String("salary", _salary)
            .ADDPARAM_CMD_String("salary_old", _salaryOld)
            .ADDPARAM_CMD_String("additional_income", _additionalIncome)
            .ADDPARAM_CMD_String("days_present", _daysPresent)
            .ADDPARAM_CMD_String("days_without_pay", _daysWithoutPay)
            .ADDPARAM_CMD_String("late_hour", _lateHour)
            .ADDPARAM_CMD_String("late_minute", _lateMinute)
            .ADDPARAM_CMD_String("ot_hour", _otHour)
            .ADDPARAM_CMD_String("ot_minute", _otMinute)
            .ADDPARAM_CMD_String("gross_amount", _grossAmount)
            .ADDPARAM_CMD_String("net_amount", _netAmount)
            .ADDPARAM_CMD_String("emp_payroll_month", _empPayrollMonth)
            .ADDPARAM_CMD_String("emp_payroll_year", _empPayrollYear)
            .ADDPARAM_CMD_String("emp_payroll_from", _empPayrollFrom)
            .ADDPARAM_CMD_String("emp_payroll_to", _empPayrollTo)
            .ADDPARAM_CMD_String("emp_payroll_period", _empPayrollPeriod)
            .ADDPARAM_CMD_String("sort_order", i + 1)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub


    Public Sub updatePayrollTransactionEmployees()
        With _clsDB.dbUtility
            .fieldItems = "additional_income,late_amount,days_without_pay_amount,salary_amount,fix_deduction_amount,deduction_amount,gross_amount,net_amount"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("additional_income", _additionalIncome)
            .ADDPARAM_CMD_String("late_amount", _lateAmount)
            .ADDPARAM_CMD_String("days_without_pay_amount", _daysWithoutPayAmount)
            .ADDPARAM_CMD_String("salary_amount", _salaryAmount)
            .ADDPARAM_CMD_String("fix_deduction_amount", _fixDeductionAmount)
            .ADDPARAM_CMD_String("deduction_amount", _deductionAmount)
            .ADDPARAM_CMD_String("gross_amount", _grossAmount)
            .ADDPARAM_CMD_String("net_amount", _netAmount)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesAllowance()
        With _clsDB.dbUtility
            .fieldItems = "allowance_permeet"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("allowance_permeet", _allowancePermeet)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub


    Public Sub updatePayrollTransactionEmployeesDiff()
        With _clsDB.dbUtility
            .fieldItems = "additional_income,late_amount,days_without_pay_amount,salary_amount,fix_deduction_amount,deduction_amount,diff_amount,gross_amount,net_amount"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("additional_income", _additionalIncome)
            .ADDPARAM_CMD_String("late_amount", _lateAmount)
            .ADDPARAM_CMD_String("days_without_pay_amount", _daysWithoutPayAmount)
            .ADDPARAM_CMD_String("salary_amount", _salaryAmount)
            .ADDPARAM_CMD_String("fix_deduction_amount", _fixDeductionAmount)
            .ADDPARAM_CMD_String("deduction_amount", _deductionAmount)
            .ADDPARAM_CMD_String("diff_amount", _diffAmount)
            .ADDPARAM_CMD_String("gross_amount", _grossAmount)
            .ADDPARAM_CMD_String("net_amount", _netAmount)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesJOES()
        With _clsDB.dbUtility
            .fieldItems = "additional_income,late_amount,ot_amount,days_without_pay_amount,salary_amount,deduction_amount,gross_amount,net_amount"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("additional_income", _additionalIncome)
            .ADDPARAM_CMD_String("late_amount", _lateAmount)
            .ADDPARAM_CMD_String("ot_amount", _otAmount)
            .ADDPARAM_CMD_String("days_without_pay_amount", _daysWithoutPayAmount)
            .ADDPARAM_CMD_String("salary_amount", _salaryAmount)
            .ADDPARAM_CMD_String("deduction_amount", _deductionAmount)
            .ADDPARAM_CMD_String("gross_amount", _grossAmount)
            .ADDPARAM_CMD_String("net_amount", _netAmount)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updateTSItemNo()

        With _clsDB.dbUtility
            With _clsDB.dbUtility
                .fieldItems = "sort_order"
                .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_header_id,transaction_employees_id", "AND")
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
                .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
                .executeUsingCommandFromSQL(True)
            End With
        End With

    End Sub

    Public Sub updateTSItemNoMultiple()

        With _clsDB.dbUtility
            With _clsDB.dbUtility
                .fieldItems = "sort_order"
                .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_header_id,employee_id", "AND")
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .executeUsingCommandFromSQL(True)
            End With
        End With

    End Sub

    Public Sub updatePayrollTransactionEmployeesWorkingDays()
        With _clsDB.dbUtility
            .fieldItems = "days_present,late_hour,late_minute"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("days_present", _daysPresent)
            .ADDPARAM_CMD_String("late_hour", _lateHour)
            .ADDPARAM_CMD_String("late_minute", _lateMinute)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesWorkingJOES()
        With _clsDB.dbUtility
            .fieldItems = "ot_hour,ot_minute"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("ot_hour", _otHour)
            .ADDPARAM_CMD_String("ot_minute", otMinute)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesDaysAbsent()
        With _clsDB.dbUtility
            .fieldItems = "days_without_pay,late_hour,late_minute,ot_hour,ot_minute"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("days_without_pay", _daysWithoutPay)
            .ADDPARAM_CMD_String("late_hour", _lateHour)
            .ADDPARAM_CMD_String("late_minute", _lateMinute)
            .ADDPARAM_CMD_String("ot_hr", _otHour)
            .ADDPARAM_CMD_String("ot_minute", _otMinute)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    


    Public Sub updatePayrollTransactionEmployeesOvertime()
        With _clsDB.dbUtility
            .fieldItems = "ot_hour,ot_amount"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("ot_hour", _otHour)
            .ADDPARAM_CMD_String("ot_amount", _otAmount)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesLeave()
        With _clsDB.dbUtility
            .fieldItems = "leave_credit,leave_amount"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("leave_credit", _leaveCredit)
            .ADDPARAM_CMD_String("leave_amount", _leaveAmount)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub


    Public Sub updatePayrollTransactionEmployeesRateMultiple()
        With _clsDB.dbUtility
            .fieldItems = "employee_position,salary"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("employee_position", _employeePosition)
            .ADDPARAM_CMD_String("salary", _salary)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesPeriod()
        With _clsDB.dbUtility
            .fieldItems = "emp_payroll_month,emp_payroll_year,emp_payroll_from,emp_payroll_to,emp_payroll_period,meeting_cnt"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("emp_payroll_month", empPayrollMonth)
            .ADDPARAM_CMD_String("emp_payroll_year", empPayrollYear)
            .ADDPARAM_CMD_String("emp_payroll_from", empPayrollFrom)
            .ADDPARAM_CMD_String("emp_payroll_to", empPayrollTo)
            .ADDPARAM_CMD_String("emp_payroll_period", empPayrollPeriod)
            .ADDPARAM_CMD_String("meeting_cnt", meetingCnt)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesRate()
        With _clsDB.dbUtility
            .fieldItems = "salary,salary_old,diff_amount"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("salary", _salary)
            .ADDPARAM_CMD_String("salary_old", _salaryOld)
            .ADDPARAM_CMD_String("diff_amount", _diffAmount)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesPosition()
        With _clsDB.dbUtility
            .fieldItems = "employee_position"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("employee_position", _employeePosition)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesNet()
        With _clsDB.dbUtility
            .fieldItems = "net_amount"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("net_amount", _netAmount)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesUtil()
        With _clsDB.dbUtility
            .fieldItems = "prev_transaction_header_id, transaction_header_id,is_active"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("prev_transaction_header_id", _prevTransactionHeaderId)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmployeesDaysMeeting()
        With _clsDB.dbUtility
            .fieldItems = "meeting_cnt,meeting_period,employee_heir"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("meeting_cnt", _meetingCnt)
            .ADDPARAM_CMD_String("meeting_period", _meetingPeriod)
            .ADDPARAM_CMD_String("meeting_heir", _employeeHeir)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updateEmployeeRemarks()
        With _clsDB.dbUtility
            .fieldItems = "emp_payroll_remarks"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("emp_payroll_remarks", _empPayrollRemarks)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub getPayrollTransactionEmployees(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_transaction_employees WHERE transaction_employees_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _transactionEmployeesId = dt.Rows(0)("transaction_employees_id").ToString
            _transactionHeaderId = dt.Rows(0)("transaction_header_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _employeeName = dt.Rows(0)("employee_name").ToString
            _employeePosition = dt.Rows(0)("employee_position").ToString
            _salary = dt.Rows(0)("salary").ToString
            _salaryOld = dt.Rows(0)("salary_old").ToString
            _additionalIncome = dt.Rows(0)("additional_income").ToString
            _daysPresent = dt.Rows(0)("days_present").ToString
            _daysWithoutPay = dt.Rows(0)("days_without_pay").ToString
            _lateHour = dt.Rows(0)("late_hour").ToString
            _lateMinute = dt.Rows(0)("late_minute").ToString
            _lateAmount = dt.Rows(0)("late_amount").ToString
            _otHour = dt.Rows(0)("ot_hour").ToString
            _otMinute = dt.Rows(0)("ot_minute").ToString
            _otAmount = dt.Rows(0)("ot_amount").ToString
            _daysWithoutPayAmount = dt.Rows(0)("days_without_pay_amount").ToString
            _salaryAmount = dt.Rows(0)("salary_amount").ToString
            _deductionAmount = dt.Rows(0)("deduction_amount").ToString
            _grossAmount = dt.Rows(0)("gross_amount").ToString
            _netAmount = dt.Rows(0)("net_amount").ToString
            _empPayrollMonth = dt.Rows(0)("emp_payroll_month").ToString
            _empPayrollYear = dt.Rows(0)("emp_payroll_year").ToString
            _empPayrollFrom = dt.Rows(0)("emp_payroll_from").ToString
            _empPayrollTo = dt.Rows(0)("emp_payroll_to").ToString
            _empPayrollPeriod = dt.Rows(0)("emp_payroll_period").ToString
            _meetingCnt = dt.Rows(0)("meeting_cnt").ToString
            _meetingPeriod = dt.Rows(0)("meeting_period").ToString
            _sortOrder = dt.Rows(0)("sort_order").ToString

        Else
            initialize()
        End If
    End Sub

    Public Sub deletePayrollTransactionEmployees(ByVal _id As String)

        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_employees WHERE transaction_employees_id='" & _id & "' LIMIT 1")

    End Sub


    Public Sub deletePayrollTransactionEmployeesAll(ByVal _id As String)

        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_employees WHERE transaction_header_id='" & _id & "'")

    End Sub


    Public Sub deletePayrollTransactionEmployeesMultiple(ByVal _thisHeaderId As String, ByVal _thisEmpId As String)

        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_employees WHERE transaction_header_id ='" & _thisHeaderId & "' AND employee_id = '" & _thisEmpId & "'")

    End Sub


    Public Sub updatePayrollTransactionEmployeesIsActive()
        With _clsDB.dbUtility
            .fieldItems = "is_active"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmpMultipleIsActive()
        With _clsDB.dbUtility
            .fieldItems = "is_active"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_header_id,employee_id", "AND")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionEmpHRAccess()
        With _clsDB.dbUtility
            .fieldItems = "hr_access"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_employees", "UPDATE", "transaction_employees_id")
            .ADDPARAM_CMD_String("hr_access", _hrAccess)
            .ADDPARAM_CMD_String("transaction_employees_id", _transactionEmployeesId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub
End Class
