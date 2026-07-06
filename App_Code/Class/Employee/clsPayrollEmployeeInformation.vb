Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollEmployeeInformation

    Dim _clsDB As New clsDatabase
    Dim _clsSecurity As New clsDimboMySQL.clsSecurity

    Dim _clsUtilSecurity As New clsUtilSecurity

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property employeeId As String

    Public Property lastName As String

    Public Property firstName As String

    Public Property middleName As String

    Public Property extName As String

    Public Property agencyId As String

    Public Property gsisNo As String

    Public Property pagibigNo As String

    Public Property philhealthNo As String

    Public Property sssNo As String

    Public Property tinNo As String

    Public Property isSeniorCitizen As String

    Public Property department As String

    Public Property subDepartment As String

    Public Property employeePosition As String

    Public Property posValDate As String

    Public Property employeePositionStep As String

    Public Property currRate As String

    Public Property employeePayrollClassification As String

    Public Property employeeStatus As String

    Public Property atmBank As String

    Public Property atmNo As String

    Public Property contactNo As String

    Public Property isActive As String

    Public Property isRetired As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property bDate As String

    Public Property payslipPassword As String

    Public Property payCheckPassword As String

    Public Property isPhilhealthExempt As String

#End Region

    Public Sub initialize()
        _employeeId = ""
        _lastName = ""
        _firstName = ""
        _middleName = ""
        _extName = ""
        _agencyId = ""
        _gsisNo = ""
        _pagibigNo = ""
        _philhealthNo = ""
        _sssNo = ""
        _tinNo = ""
        _isSeniorCitizen = "N"
        _department = ""
        _subDepartment = "NONE"
        _employeePosition = ""
        _posValDate = "0000-00-00"
        _employeePositionStep = "1"
        _currRate = "0.00"
        _employeePayrollClassification = ""
        _employeeStatus = ""
        _atmBank = ""
        _atmNo = ""
        _contactNo = ""
        _isActive = ""
        _createUser = ""
        _createDate = ""
        _bDate = "0000-00-00"
        _payslipPassword = ""
        _isPhilhealthExempt = "N"
    End Sub

    Public Function browsePayrollEmployeeInformationForLog(ByVal _thisID As String) As DataTable
        Dim sql As String = ""

        'sql = "SELECT last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, is_senior_citizen, department, position_name AS employee_position, employee_position_step, employee_payroll_classification,contact_no,atm_bank,atm_no,is_retired FROM tbl_payroll_employee_information " & _
        '      "LEFT JOIN tbl_payroll_ref_employee_position ON tbl_payroll_employee_information.employee_position = tbl_payroll_ref_employee_position.position_id " & _
        '      " WHERE employee_id='" & _thisID & "' LIMIT 1"

        sql = "SELECT last_name, first_name, middle_name, ext_name, tbl_payroll_employee_information.agency_id AS employeeNo, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, is_senior_citizen, department, position_name AS employee_position, " & _
              "employee_position_step,employee_payroll_classification,employee_status,contact_no,atm_bank,atm_no,is_retired,tbl_payroll_employee_information.is_active,DATE_FORMAT(bdate,'%m/%d/%Y') AS bdate, " & _
              "GROUP_CONCAT((CASE WHEN tbl_payroll_employee_agency_id.agency_id_no <> '' THEN CONCAT(tbl_payroll_employee_agency_id.agency_id,':',tbl_payroll_employee_agency_id.agency_id_no) ELSE NULL END)) AS policyNumbers,is_philhealth_exempt " & _
              "FROM tbl_payroll_employee_information " & _
              "LEFT JOIN tbl_payroll_ref_employee_position ON tbl_payroll_employee_information.employee_position = tbl_payroll_ref_employee_position.position_id " & _
              "LEFT JOIN tbl_payroll_employee_agency_id ON tbl_payroll_employee_information.employee_id = tbl_payroll_employee_agency_id.employee_id " & _
              "WHERE tbl_payroll_employee_information.employee_id='" & _thisID & "' GROUP BY tbl_payroll_employee_information.employee_id LIMIT 1"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_information")
    End Function

    Public Function browsePayrollEmployeeInformation(ByVal _criteria As String, Optional _thisDepartment As String = "", Optional _thisClassification As String = "", Optional _thisList As String = "OFFICE") As DataTable
        Dim sql As String = ""
        Dim params() As String = Regex.Split(_criteria, "zxz")

        Dim _where As String = ""

        If params(0) <> "" Then
            _where = "AND last_name LIKE '%" & params(0) & "%' "
        End If

        If params(1).Trim <> "" Then
            'If _where = "" Then
            '    _where = " WHERE first_name LIKE '%" & params(1).Trim & "%'"
            'Else
            _where += " AND first_name LIKE '%" & params(1).Trim & "%' "
            ' End If
        End If

        If _thisClassification <> "" Then
            _where += "AND employee_payroll_classification = '" & _thisClassification & "' "
        End If

        If _thisList = "OFFICE" Then

            If _thisDepartment <> "" Then
                'If _where = "" Then
                '    _where = " WHERE department = '" & _thisDepartment & "' "
                'Else
                _where += " AND department = '" & _thisDepartment & "' "
                ' End If
            End If

            sql = "SELECT employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, " & _
           "is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification,employee_status,is_active,(CASE WHEN sub_department <> 'NONE' THEN 'SECTOR' ELSE 'OFFICE' END) AS listType FROM tbl_payroll_employee_information " & _
           "WHERE is_active <> '' AND sub_department = 'NONE' " & _where & _
           "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        ElseIf _thisList = "DETAILED" Then

            ' If _thisClassification = "JOB ORDER" Then
            sql = "SELECT tbl_payroll_employee_information.employee_id, last_name, first_name, middle_name, ext_name,position_name,agency_id, gsis_no, pagibig_no, philhealth_no, " & _
            "sss_no, tin_no,is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification,employee_status,tbl_payroll_employee_information.is_active,(CASE WHEN sub_department <> 'NONE' THEN 'SECTOR' ELSE 'OFFICE' END) AS listType FROM tbl_payroll_other_employee " & _
            "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_other_employee.employee_id = tbl_payroll_employee_information.employee_id " & _
            "INNER JOIN tbl_payroll_ref_employee_position ON tbl_payroll_employee_information.employee_position  = tbl_payroll_ref_employee_position.position_id " & _
            "WHERE department_to='" & _thisDepartment & "' " & _where & _
            "ORDER BY last_name,first_name LIMIT " & params(2).Trim
            'End If

        End If

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_information")

    End Function

    Public Function browsePayrollEmployeeInformationHRNetIncome(ByVal _criteria As String, Optional _thisDepartment As String = "", Optional _thisClassification As String = "") As DataTable
        Dim sql As String = ""
        Dim params() As String = Regex.Split(_criteria, "zxz")

        Dim _where As String = ""

        If params(0) <> "" Then
            _where = "AND last_name LIKE '%" & params(0) & "%' "
        End If

        If params(1).Trim <> "" Then
            _where += " AND first_name LIKE '%" & params(1).Trim & "%' "
        End If

        If _thisClassification <> "" Then
            _where += "AND employee_payroll_classification = '" & _thisClassification & "' "
        End If

        If _thisDepartment <> "" Then
            _where += " AND department = '" & _thisDepartment & "' "
        End If

        'sql = "SELECT employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, " & _
        ' "is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification,employee_status,is_active,(CASE WHEN sub_department <> 'NONE' THEN 'SECTOR' ELSE 'OFFICE' END) AS listType FROM tbl_payroll_employee_information " & _
        ' "WHERE is_active <> '' AND sub_department = 'NONE' " & _where & _
        ' "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        sql = "SELECT tbl_payroll_employee_information.employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, " & _
              "sss_no, tin_no, is_senior_citizen, department, tbl_payroll_transaction_employees.employee_position, employee_position_step, employee_payroll_classification, " & _
              "employee_status,(CASE WHEN sub_department <> 'NONE' THEN 'SECTOR' ELSE 'OFFICE' END) AS listType, " & _
              "MAX(CONCAT(tbl_payroll_transaction_employees.emp_payroll_year,tbl_payroll_transaction_employees.emp_payroll_month)) AS period " & _
              "FROM tbl_payroll_employee_information " & _
              "INNER JOIN tbl_payroll_transaction_employees ON tbl_payroll_employee_information.employee_id = tbl_payroll_transaction_employees.employee_id AND " & _
              "tbl_payroll_transaction_employees.is_active = 'Y' AND tbl_payroll_transaction_employees.hr_access = 'Y' " & _
              "WHERE tbl_payroll_employee_information.is_active <> '' AND sub_department = 'NONE' " & _where & _
              "GROUP BY tbl_payroll_employee_information.employee_id " & _
              "ORDER BY MAX(tbl_payroll_transaction_employees.last_date) DESC, " & _
              "MAX(CONCAT(tbl_payroll_transaction_employees.emp_payroll_year,tbl_payroll_transaction_employees.emp_payroll_month)) DESC " & _
              "LIMIT " & params(2).Trim

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_information")

    End Function

    Public Function browsePayrollEmployeeInformationLedger(ByVal _criteria As String, Optional _thisDepartment As String = "", Optional _thisClassification As String = "", Optional _thisList As String = "OFFICE") As DataTable
        Dim sql As String = ""
        Dim params() As String = Regex.Split(_criteria, "zxz")

        Dim _where As String = ""

        If params(0) <> "" Then
            _where = "AND last_name LIKE '%" & params(0) & "%' "
        End If

        If params(1).Trim <> "" Then
            'If _where = "" Then
            '    _where = " WHERE first_name LIKE '%" & params(1).Trim & "%'"
            'Else
            _where += " AND first_name LIKE '%" & params(1).Trim & "%' "
            ' End If
        End If

        If _thisClassification <> "" Then
            _where += "AND employee_payroll_classification = '" & _thisClassification & "' "
        End If

        If _thisList = "OFFICE" Then

            If _thisDepartment <> "" Then
                'If _where = "" Then
                '    _where = " WHERE department = '" & _thisDepartment & "' "
                'Else
                _where += " AND department = '" & _thisDepartment & "' "
                ' End If
            End If

            sql = "SELECT employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, " & _
           "is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification,employee_status,is_active,(CASE WHEN sub_department <> 'NONE' THEN 'SECTOR' ELSE 'OFFICE' END) AS listType FROM tbl_payroll_employee_information " & _
           "WHERE is_active <> '' " & _where & _
           "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        ElseIf _thisList = "DETAILED" Then

            ' If _thisClassification = "JOB ORDER" Then
            sql = "SELECT tbl_payroll_employee_information.employee_id, last_name, first_name, middle_name, ext_name,position_name,agency_id, gsis_no, pagibig_no, philhealth_no, " & _
            "sss_no, tin_no,is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification,employee_status,tbl_payroll_employee_information.is_active,(CASE WHEN sub_department <> 'NONE' THEN 'SECTOR' ELSE 'OFFICE' END) AS listType FROM tbl_payroll_other_employee " & _
            "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_other_employee.employee_id = tbl_payroll_employee_information.employee_id " & _
            "INNER JOIN tbl_payroll_ref_employee_position ON tbl_payroll_employee_information.employee_position  = tbl_payroll_ref_employee_position.position_id " & _
            "WHERE department_to='" & _thisDepartment & "' " & _where & _
            "ORDER BY last_name,first_name LIMIT " & params(2).Trim
            'End If

        End If

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_information")

    End Function

    Public Function browsePayrollEmployeeInformationATM(ByVal _criteria As String, Optional _thisDepartment As String = "", Optional _thisClassification As String = "", Optional _thisList As String = "OFFICE") As DataTable
        Dim sql As String = ""
        Dim params() As String = Regex.Split(_criteria, "zxz")

        Dim _where As String = ""

        If params(0) <> "" Then
            _where = "AND last_name LIKE '%" & params(0) & "%' "
        End If

        If params(1).Trim <> "" Then
            'If _where = "" Then
            '    _where = " WHERE first_name LIKE '%" & params(1).Trim & "%'"
            'Else
            _where += " AND first_name LIKE '%" & params(1).Trim & "%' "
            ' End If
        End If

        If _thisClassification <> "" Then
            _where += "AND employee_payroll_classification = '" & _thisClassification & "' "
        End If

        If _thisList = "OFFICE" Then

            If _thisDepartment <> "" Then
                _where += " AND department = '" & _thisDepartment & "' "
            End If

            sql = "SELECT employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, " & _
           "is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification,employee_status,tbl_payroll_ref_department.is_active, " & _
           "(CASE WHEN sub_department <> 'NONE' THEN 'SECTOR' ELSE 'OFFICE' END) AS listType, " & _
           "CONCAT(last_name,', ',first_name,' ',ext_name, ' ', middle_name) AS empName, " & _
           "atm_bank, atm_no, tbl_payroll_ref_department.deptcode FROM tbl_payroll_employee_information " & _
           "INNER JOIN tbl_payroll_ref_department ON tbl_payroll_employee_information.department = tbl_payroll_ref_department.department_id " & _
           "WHERE tbl_payroll_employee_information.is_active <> '' AND department <> 'NONE' " & _where & _
           "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        ElseIf _thisList = "DETAILED" Then

            ' If _thisClassification = "JOB ORDER" Then
            sql = "SELECT tbl_payroll_employee_information.employee_id, last_name, first_name, middle_name, ext_name,position_name,agency_id, gsis_no, pagibig_no, philhealth_no, " & _
            "sss_no, tin_no,is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification,employee_status FROM tbl_payroll_other_employee " & _
            "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_other_employee.employee_id = tbl_payroll_employee_information.employee_id " & _
            "INNER JOIN tbl_payroll_ref_employee_position ON tbl_payroll_employee_information.employee_position  = tbl_payroll_ref_employee_position.position_id " & _
            "WHERE department_to='" & _thisDepartment & "' " & _where & _
            "ORDER BY last_name,first_name LIMIT " & params(2).Trim
            'End If

        End If

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_information")

    End Function




    Public Function browsePayrollEmployeeInformationHR(ByVal _criteria As String, Optional _thisDepartment As String = "", Optional _thisClassification As String = "") As DataTable
        Dim sql As String = ""
        Dim params() As String = Regex.Split(_criteria, "zxz")

        Dim _where As String = ""

        If params(0) <> "" Then
            _where = "AND last_name LIKE '%" & params(0) & "%' "
        End If

        If params(1).Trim <> "" Then
            'If _where = "" Then
            '    _where = " WHERE first_name LIKE '%" & params(1).Trim & "%'"
            'Else
            _where += " AND first_name LIKE '%" & params(1).Trim & "%' "
            ' End If
        End If

        If _thisDepartment <> "" Then
            'If _where = "" Then
            '    _where = " WHERE department = '" & _thisDepartment & "' "
            'Else
            _where += " AND department = '" & _thisDepartment & "' "
            ' End If
        End If

        If _thisClassification <> "" Then
            _where += "AND employee_payroll_classification = '" & _thisClassification & "' "
        End If

        'sql = "SELECT tbl_payroll_employee_information.employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, " & _
        '      "is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification, deduction_amount " & _
        '      "FROM tbl_payroll_employee_information " & _
        '      "LEFT JOIN tbl_payroll_employee_fix_deductions ON tbl_payroll_employee_information.employee_id = tbl_payroll_employee_fix_deductions.employee_id AND " & _
        '      "tbl_payroll_employee_fix_deductions.fix_deduction_id = 'WITHHOLDING TAX' " & _
        '      "WHERE is_active <> '' " & _where & _
        '      "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        sql = "SELECT employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no,employee_status, " & _
            "is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification,tbl_payroll_employee_information.is_active,department_name,position_name FROM tbl_payroll_employee_information " & _
            "LEFT JOIN tbl_payroll_ref_employee_position ON tbl_payroll_employee_information.employee_position = tbl_payroll_ref_employee_position.position_id " & _
            "LEFT JOIN tbl_payroll_ref_department ON tbl_payroll_employee_information.department = tbl_payroll_ref_department.department_id " & _
            "WHERE tbl_payroll_employee_information.is_active <> '' AND sub_department = 'NONE' " & _where & _
            "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_information")

    End Function

    Public Function browseEmployeeOffice(ByVal _criteria As String, Optional _thisDepartment As String = "", Optional _thisClassification As String = "") As DataTable
        Dim sql As String = ""
        Dim params() As String = Regex.Split(_criteria, "zxz")

        Dim _where As String = ""

        If params(0) <> "" Then
            _where = "AND last_name LIKE '%" & params(0) & "%' "
        End If

        If params(1).Trim <> "" Then
            'If _where = "" Then
            '    _where = " WHERE first_name LIKE '%" & params(1).Trim & "%'"
            'Else
            _where += " AND first_name LIKE '%" & params(1).Trim & "%' "
            ' End If
        End If

        If _thisDepartment <> "" Then
            'If _where = "" Then
            '    _where = " WHERE department = '" & _thisDepartment & "' "
            'Else
            _where += " AND department = '" & _thisDepartment & "' "
            ' End If
        End If

        If _thisClassification <> "" Then
            _where += "AND employee_payroll_classification = '" & _thisClassification & "' "
        End If

        'sql = "SELECT tbl_payroll_employee_information.employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, " & _
        '      "is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification, deduction_amount " & _
        '      "FROM tbl_payroll_employee_information " & _
        '      "LEFT JOIN tbl_payroll_employee_fix_deductions ON tbl_payroll_employee_information.employee_id = tbl_payroll_employee_fix_deductions.employee_id AND " & _
        '      "tbl_payroll_employee_fix_deductions.fix_deduction_id = 'WITHHOLDING TAX' " & _
        '      "WHERE is_active <> '' " & _where & _
        '      "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        sql = "SELECT employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, department_name, " & _
            "is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification,tbl_payroll_employee_information.is_active FROM tbl_payroll_employee_information " & _
            "INNER JOIN tbl_payroll_ref_department ON tbl_payroll_employee_information.department = tbl_payroll_ref_department.department_id " & _
            "WHERE tbl_payroll_employee_information.is_active <> '' " & _where & _
            "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_information")

    End Function

    Public Function browsePayrollSectorEmployeeInformation(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        Dim params() As String = Regex.Split(_criteria, "zxz")

        Dim _where As String = ""

        If params(0) <> "" Then
            _where = "AND last_name LIKE '%" & params(0) & "%' "
        End If

        If params(1).Trim <> "" Then
            'If _where = "" Then
            '    _where = " WHERE first_name LIKE '%" & params(1).Trim & "%'"
            'Else
            _where += " AND first_name LIKE '%" & params(1).Trim & "%' "
            ' End If
        End If

        If params(3).Trim <> "" Then
            'If _where = "" Then
            '    _where = " WHERE department = '" & _thisDepartment & "' "
            'Else
            _where += " AND department = '" & params(3).Trim & "' "
            ' End If
        End If

        If params(4).Trim <> "" Then
            _where += "AND sub_department = '" & params(4).Trim & "' "
        Else
            _where += "AND sub_department <> 'NONE' "
        End If

        sql = "SELECT employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, " & _
            "is_senior_citizen, department,position_name AS employee_position,employee_position_step, employee_payroll_classification,is_active FROM tbl_payroll_employee_information " & _
            "LEFT JOIN tbl_payroll_ref_employee_position ON tbl_payroll_employee_information.employee_position = tbl_payroll_ref_employee_position. position_id " & _
            "WHERE is_active <> '' " & _where & _
            "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_information")

    End Function

    Public Function browsePayrollEmployeeInformationWtax(ByVal _criteria As String, Optional _thisDepartment As String = "", Optional _thisClassification As String = "") As DataTable
        Dim sql As String = ""
        Dim params() As String = Regex.Split(_criteria, "zxz")

        Dim _where As String = ""

        If params(0) <> "" Then
            _where = "AND last_name LIKE '%" & params(0) & "%' "
        End If

        If params(1).Trim <> "" Then
            'If _where = "" Then
            '    _where = " WHERE first_name LIKE '%" & params(1).Trim & "%'"
            'Else
            _where += " AND first_name LIKE '%" & params(1).Trim & "%' "
            ' End If
        End If

        If _thisClassification <> "" Then
            _where += "AND employee_payroll_classification = '" & _thisClassification & "' "
        End If

        sql = "SELECT tbl_payroll_employee_information.employee_id, last_name, first_name, middle_name, ext_name, agency_id, gsis_no, pagibig_no, philhealth_no, sss_no, tin_no, " & _
              "is_senior_citizen, department, employee_position, employee_position_step, employee_payroll_classification, deduction_amount " & _
              "FROM tbl_payroll_employee_information " & _
              "LEFT JOIN tbl_payroll_employee_fix_deductions ON tbl_payroll_employee_information.employee_id = tbl_payroll_employee_fix_deductions.employee_id AND " & _
              "tbl_payroll_employee_fix_deductions.fix_deduction_id = 'WITHHOLDING TAX' " & _
              "WHERE is_active <> '' " & _where & _
              "ORDER BY last_name,first_name LIMIT " & params(2).Trim

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_information")
    End Function


    Public Sub savePayrollEmployeeInformation()
        If employeeId = "" Then

            With _clsDB.dbUtility
                .fieldItems = "employee_id,last_name,first_name,middle_name,ext_name,agency_id,gsis_no,pagibig_no,philhealth_no,sss_no,tin_no,is_senior_citizen,department,sub_department,employee_position,employee_position_step,employee_payroll_classification,employee_status,contact_no,bdate,is_philhealth_exempt,is_active,create_user"
                .sqlString = .getSQLStatement("tbl_payroll_employee_information", "INSERT")
                _employeeId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("last_name", _lastName)
                .ADDPARAM_CMD_String("first_name", _firstName)
                .ADDPARAM_CMD_String("middle_name", _middleName)
                .ADDPARAM_CMD_String("ext_name", _extName)
                .ADDPARAM_CMD_String("agency_id", _agencyId)
                .ADDPARAM_CMD_String("gsis_no", _gsisNo)
                .ADDPARAM_CMD_String("pagibig_no", _pagibigNo)
                .ADDPARAM_CMD_String("philhealth_no", _philhealthNo)
                .ADDPARAM_CMD_String("sss_no", _sssNo)
                .ADDPARAM_CMD_String("tin_no", _tinNo)
                .ADDPARAM_CMD_String("is_senior_citizen", _isSeniorCitizen)
                .ADDPARAM_CMD_String("department", _department)
                .ADDPARAM_CMD_String("sub_department", _subDepartment)
                .ADDPARAM_CMD_String("employee_position", _employeePosition)
                .ADDPARAM_CMD_String("employee_position_step", _employeePositionStep)
                .ADDPARAM_CMD_String("employee_payroll_classification", _employeePayrollClassification)
                .ADDPARAM_CMD_String("employee_status", _employeeStatus)
                .ADDPARAM_CMD_String("contact_no", _contactNo)
                .ADDPARAM_CMD_String("bdate", _bDate)
                .ADDPARAM_CMD_String("is_philhealth_exempt", _isPhilhealthExempt)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", createUser)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "last_name,first_name,middle_name,ext_name,agency_id,gsis_no,pagibig_no,philhealth_no,sss_no,tin_no,is_senior_citizen,department,sub_department,employee_position,employee_position_step,employee_payroll_classification,employee_status,contact_no,bdate,is_philhealth_exempt,is_active"
                .sqlString = .getSQLStatement("tbl_payroll_employee_information", "UPDATE", "employee_id")
                .ADDPARAM_CMD_String("last_name", _lastName)
                .ADDPARAM_CMD_String("first_name", _firstName)
                .ADDPARAM_CMD_String("middle_name", _middleName)
                .ADDPARAM_CMD_String("ext_name", _extName)
                .ADDPARAM_CMD_String("agency_id", _agencyId)
                .ADDPARAM_CMD_String("gsis_no", _gsisNo)
                .ADDPARAM_CMD_String("pagibig_no", _pagibigNo)
                .ADDPARAM_CMD_String("philhealth_no", _philhealthNo)
                .ADDPARAM_CMD_String("sss_no", _sssNo)
                .ADDPARAM_CMD_String("tin_no", _tinNo)
                .ADDPARAM_CMD_String("is_senior_citizen", _isSeniorCitizen)
                .ADDPARAM_CMD_String("department", _department)
                .ADDPARAM_CMD_String("sub_department", _subDepartment)
                .ADDPARAM_CMD_String("employee_position", _employeePosition)
                .ADDPARAM_CMD_String("employee_position_step", _employeePositionStep)
                .ADDPARAM_CMD_String("employee_payroll_classification", _employeePayrollClassification)
                .ADDPARAM_CMD_String("employee_status", _employeeStatus)
                .ADDPARAM_CMD_String("contact_no", _contactNo)
                .ADDPARAM_CMD_String("bdate", _bDate)
                .ADDPARAM_CMD_String("is_philhealth_exempt", _isPhilhealthExempt)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub updateEmployeeOffice()

        With _clsDB.dbUtility
            .fieldItems = "department"
            .sqlString = .getSQLStatement("tbl_payroll_employee_information", "UPDATE", "employee_id")
            .ADDPARAM_CMD_String("department", _department)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollEmployeeATM()

        With _clsDB.dbUtility
            .fieldItems = "atm_bank, atm_no"
            .sqlString = .getSQLStatement("tbl_payroll_employee_information", "UPDATE", "employee_id")
            .ADDPARAM_CMD_String("atm_bank", _atmBank)
            .ADDPARAM_CMD_String("atm_no", _atmNo)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updateEmployeePosition()

        With _clsDB.dbUtility
            .fieldItems = "employee_position,pos_valdate,employee_position_step,curr_rate"
            .sqlString = .getSQLStatement("tbl_payroll_employee_information", "UPDATE", "employee_id")
            .ADDPARAM_CMD_String("employee_position", _employeePosition)
            .ADDPARAM_CMD_String("pos_valdate", _posValDate)
            .ADDPARAM_CMD_String("employee_position_step", _employeePositionStep)
            .ADDPARAM_CMD_String("curr_rate", _currRate)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePaycheckPass()
        With _clsDB.dbUtility
            .fieldItems = "paycheck_password"
            .sqlString = .getSQLStatement("tbl_payroll_employee_information", "UPDATE", "employee_id")

            .ADDPARAM_CMD_String("paycheck_password", _clsUtilSecurity.encryptString(_payCheckPassword))
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub getPayrollEmployeeInformation(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_information WHERE employee_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _employeeId = dt.Rows(0)("employee_id").ToString
            _lastName = dt.Rows(0)("last_name").ToString
            _firstName = dt.Rows(0)("first_name").ToString
            _middleName = dt.Rows(0)("middle_name").ToString
            _extName = dt.Rows(0)("ext_name").ToString
            _agencyId = dt.Rows(0)("agency_id").ToString
            _gsisNo = dt.Rows(0)("gsis_no").ToString
            _pagibigNo = dt.Rows(0)("pagibig_no").ToString
            _philhealthNo = dt.Rows(0)("philhealth_no").ToString
            _sssNo = dt.Rows(0)("sss_no").ToString
            _tinNo = dt.Rows(0)("tin_no").ToString
            _isSeniorCitizen = dt.Rows(0)("is_senior_citizen").ToString
            _department = dt.Rows(0)("department").ToString
            _subDepartment = dt.Rows(0)("sub_department").ToString
            _employeePosition = dt.Rows(0)("employee_position").ToString
            _employeePositionStep = dt.Rows(0)("employee_position_step").ToString
            _currRate = dt.Rows(0)("curr_rate").ToString
            _employeePayrollClassification = dt.Rows(0)("employee_payroll_classification").ToString
            _employeeStatus = dt.Rows(0)("employee_status").ToString
            _atmBank = dt.Rows(0)("atm_bank").ToString
            _atmNo = dt.Rows(0)("atm_no").ToString
            _contactNo = dt.Rows(0)("contact_no").ToString
            _isActive = dt.Rows(0)("is_active").ToString
            _bDate = dt.Rows(0)("bdate").ToString
            _payslipPassword = dt.Rows(0)("payslip_password").ToString
            _isPhilhealthExempt = dt.Rows(0)("is_philhealth_exempt").ToString

        Else
            initialize()
        End If
    End Sub


    Public Sub getPayrollPayslipAccount(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_information WHERE agency_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _agencyId = dt.Rows(0)("agency_id").ToString
            _payCheckPassword = dt.Rows(0)("paycheck_password").ToString

        Else
            initialize()
        End If
    End Sub


    Public Function getEmployeePayrollSalaryJobOrder(_id As String) As Decimal
        Dim sql As String = "SELECT daily_wage FROM tbl_payroll_employee_information " & _
                            "INNER JOIN tbl_payroll_ref_employee_position ON tbl_payroll_employee_information.employee_position  = tbl_payroll_ref_employee_position.position_id " & _
                            "INNER JOIN tbl_payroll_ref_salary_daily ON tbl_payroll_ref_employee_position.salary_grade = tbl_payroll_ref_salary_daily.salary_grade " & _
                           "WHERE tbl_payroll_employee_information.employee_id='" & _id & "'"

        Dim salary As Decimal = _clsDB.Get_DB_Item(sql)


        Return salary
    End Function

    Public Function validateLogin(ByVal _struserid As String, ByVal _strpassword As String) As Boolean

        Dim dt As New DataTable

        getPayrollPayslipAccount(_struserid)

        dt = _clsDB.Fill_DataTable("SELECT (CASE WHEN MD5('" & _strpassword & "') = '" & _payCheckPassword & "' THEN 'TRUE' ELSE 'FALSE' END)")

        Dim bol As Boolean = False

        If dt.Rows.Count > 0 Then
            bol = IIf(dt.Rows(0)(0) = "TRUE", True, False)
        End If

        If bol = False Then
            bol = _clsSecurity.validateSameText(_strpassword, _payCheckPassword)
        End If

        Return bol

    End Function

    Public Function getEmployeePayrollSalaryRegular(_id As String) As Decimal
        Dim sql As String = "SELECT salary FROM tbl_payroll_employee_information " & _
                            "INNER JOIN tbl_payroll_ref_employee_position ON tbl_payroll_employee_information.employee_position  = tbl_payroll_ref_employee_position.position_id " & _
                            "INNER JOIN tbl_payroll_ref_salary_regular ON tbl_payroll_ref_employee_position.salary_grade = tbl_payroll_ref_salary_regular.salary_grade " & _
                            " AND  tbl_payroll_employee_information.employee_position_step = tbl_payroll_ref_salary_regular.salary_step " & _
                           "WHERE tbl_payroll_employee_information.employee_id='" & _id & "'"

        Dim salary As Decimal = _clsDB.Get_DB_Item(sql)


        Return salary
    End Function
End Class
