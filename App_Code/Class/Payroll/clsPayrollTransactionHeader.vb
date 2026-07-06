Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollTransactionHeader

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property transactionHeaderId As String

    Public Property referenceCode As String

    Public Property cadNo As String

    Public Property payrollClassification As String

    Public Property payrollCompensation As String

    Public Property payrollCompensationType As String

    Public Property payrollFundSource As String

    Public Property department As String


    Public Property payrollMode As String

    Public Property payrollMonth As String

    Public Property payrollYear As String

    Public Property payrollFrom As String

    Public Property payrollTo As String

    Public Property payrollPeriod As String
    Public Property payrollDetails As String

    Public Property employeeCount As String

    Public Property payrollGrossAmount As String

    Public Property payrollNetAmount As String

    Public Property payrollStatus As String

    Public Property transmittalDate As String

    Public Property transmittalRemarks As String

    Public Property processCount As String

    Public Property postingCount As String

    Public Property isActive As String

    Public Property isMultiple As String

    Public Property isPosted As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property payrollRemarks As String

    Public Property bookedDate As String

    Public Property auditedDate As String

    Public Property voucherDesc As String

#End Region

    Public Sub initialize()

        _transactionHeaderId = ""
        _referenceCode = ""
        _cadNo = ""
        _payrollClassification = ""
        _payrollCompensation = ""
        _payrollCompensationType = "SALARY"
        _payrollFundSource = ""
        _department = ""
        _payrollMode = ""
        _payrollMonth = "0"
        _payrollYear = ""
        _payrollFrom = "0000-00-00"
        _payrollTo = "0000-00-00"
        _payrollPeriod = ""
        _payrollDetails = ""
        _employeeCount = "0"
        _payrollGrossAmount = "0.00"
        _payrollNetAmount = "0.00"
        _payrollStatus = "PREPARATION"
        _transmittalDate = ""
        _transmittalRemarks = ""
        _processCount = "0"
        _postingCount = "0"
        _isActive = "Y"
        _isPosted = "N"
        _createUser = ""
        _createDate = ""
        _isMultiple = "N"
        _payrollRemarks = ""
        _bookedDate = Nothing
        _auditedDate = Nothing
        _voucherDesc = ""

    End Sub

    Public Function browsePayrollTransactionHeaderForLog(ByVal _thisID As String) As DataTable

        Dim sql As String = ""
        sql = "SELECT reference_code, payroll_classification, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status FROM tbl_payroll_transaction_header " & _
              " WHERE transaction_header_id='" & _thisID & "' LIMIT 1"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")

    End Function

    Public Function browsePayrollTransactionHeaderProcess(ByVal _payrollType As String, ByVal _thisPayrollMode As String, ByVal _thisPeriodMonth As String, ByVal _thisPeriodYear As String) As DataTable

        Dim sql As String = ""

        Dim _where As String = ""

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted FROM tbl_payroll_transaction_header " & _
              "WHERE is_active = 'Y' " & _where

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")

    End Function

    Public Function browsePayrollTransactionHeaderBookList(ByVal _thisCadNo As String, ByVal _thisPayrollType As String, ByVal _thisPeriodMonth As String, ByVal _thisPeriodYear As String, ByVal _thisStatus As String, Optional _thisCompensation As String = "") As DataTable
        Dim sql As String = ""

        Dim _where As String = ""

        If _thisCadNo <> "" Then

            _where += "AND cad_no = '" & _thisCadNo & "' "

        End If

        If _thisPayrollType <> "" Then

            _where += "AND payroll_compensation = '" & _thisPayrollType & "' "

        End If

        If _thisPeriodMonth <> "" Then

            _where += "AND payroll_month = '" & _thisPeriodMonth & "' "

        End If

        If _thisPeriodYear <> "" Then

            _where += "AND payroll_year = '" & _thisPeriodYear & "' "

        End If

        If _thisStatus <> "" Then
            'If _thisStatus = "" Then
            _where += "AND payroll_status IN (" & _thisStatus & ") "
            'Else
            '    _where += "AND payroll_status = '" & _thisStatus & "' "
            'End If
        End If

        If _thisCompensation <> "" Then
            _where += "AND payroll_compensation = '" & _thisCompensation & "' "
        End If

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation_type, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted,cad_no FROM tbl_payroll_transaction_header " & _
              "WHERE is_active = 'Y' " & _where

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")

    End Function

    Public Function browsePayrollTransactionHeaderCadNo(ByVal _thisCadNo As String, Optional _thisYear As String = "") As DataTable
        Dim sql As String = ""

        Dim _where As String = ""

        If _thisCadNo <> "" Then

            _where += "AND cad_no = '" & _thisCadNo & "' "

        End If

        If _thisYear <> "" Then
            _where += "AND  payroll_year = '" & _thisYear & "' "
        End If


        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation_type, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted,cad_no FROM tbl_payroll_transaction_header " & _
              "WHERE is_active = 'Y' " & _where & " AND payroll_status IN ('POSTED','BOOKED') LIMIT 1"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")

    End Function

    Public Function browsePayrollTransactionHeaderList(ByVal _criteria As String, thisPayrollClass As String, thisDepartment As String, Optional _thisSearch As String = "", Optional _thisMultipe As String = "N") As DataTable

        Dim sql As String = ""

        Dim _where As String = " WHERE payroll_compensation_type = 'SALARY' AND department = '" & thisDepartment & "' "

        If thisPayrollClass <> "" Then
            _where += "AND payroll_classification = '" & thisPayrollClass & "' "
        End If

        Dim splitCriteria As String()

        splitCriteria = Split(_criteria, "zxz")

        If splitCriteria.Length > 0 Then
            If splitCriteria(0) <> "" And splitCriteria(0) <> "ALL" Then
                _where += "AND payroll_mode = '" & splitCriteria(0) & "' "
            End If

            If splitCriteria(1) = "Y" Then
                If splitCriteria(2) Then
                    _where += "AND payroll_month = '" & splitCriteria(2) & "' "
                End If
                If splitCriteria(3) Then
                    _where += "AND payroll_year = '" & splitCriteria(3) & "' "
                End If
            End If

        End If

        If _thisMultipe <> "" Then
            _where += "AND is_multiple = '" & _thisMultipe & "' "
        End If

        If _thisSearch <> "" Then
            _where += "AND reference_code LIKE '%" & _thisSearch & "%' OR payroll_details LIKE '%" & _thisSearch & "%'  "

        End If

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, payroll_compensation_type, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted FROM tbl_payroll_transaction_header " & _
              _where & " ORDER BY payroll_to DESC"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")
    End Function

    Public Function browsePayrollTransactionHeaderPrintList(ByVal _criteria As String, thisPayrollClass As String, thisDepartment As String, ByVal _thisTop As String) As DataTable

        Dim sql As String = ""

        Dim _where As String = " WHERE payroll_compensation_type = 'SALARY' AND payroll_status <> 'PREPARATION' AND tbl_payroll_transaction_header.is_active = 'Y' "

        If thisDepartment <> "" Then
            _where += "AND department = '" & thisDepartment & "'"
        End If

        If thisPayrollClass <> "" Then

            _where += "AND payroll_classification = '" & thisPayrollClass & "' "

        End If


        Dim splitCriteria As String()

        splitCriteria = Split(_criteria, "zxz")

        If splitCriteria.Length > 0 Then

            If splitCriteria(0) <> "" Then
                _where += "AND payroll_mode = '" & splitCriteria(0) & "' "
            End If

            If splitCriteria(1) = "Y" Then
                If splitCriteria(2) Then
                    _where += "AND payroll_month = '" & splitCriteria(2) & "' "
                End If
                If splitCriteria(3) Then
                    _where += "AND payroll_year = '" & splitCriteria(3) & "' "
                End If
            End If

        End If

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, payroll_compensation_type, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_posted, department_name FROM tbl_payroll_transaction_header " & _
              "INNER JOIN tbl_payroll_ref_department ON tbl_payroll_transaction_header.department = tbl_payroll_ref_department.department_id " & _
              _where & _
              "ORDER BY create_date DESC LIMIT " & _thisTop

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")
    End Function


    Public Function browsePayrollForReceive(ByVal _thisRef As String, thisStatus As String) As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""


        If _thisRef <> "" Then
            sqlWhere = "AND (reference_code LIKE '" & _thisRef & "%' OR cad_no LIKE '" & _thisRef & "%')  "
        End If

        If thisStatus <> "" Then

            If thisStatus = "RECEIVED" Then
                sqlWhere += "AND payroll_status NOT IN ('PREPARATION','SUBMIT TO ACCOUNTING')"
            Else
                sqlWhere += "AND payroll_status = '" & thisStatus & "'"
            End If

        End If

        sql = "SELECT transaction_header_id, reference_code, cad_no, payroll_classification, payroll_compensation, payroll_compensation_type, department, payroll_mode, payroll_month, payroll_year, " & _
           "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
           "transmittal_remarks, process_count, posting_count, is_active, is_posted FROM tbl_payroll_transaction_header " & _
           "WHERE is_active = 'Y' " & sqlWhere & _
           "ORDER BY payroll_from DESC "


        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")
    End Function

    Public Function browsePayrollTransactionHeaderType(ByVal _criteria As String, thisPayrollClass As String, _thisCompType As String) As DataTable

        Dim sql As String = ""

        Dim _where As String = " WHERE payroll_compensation_type = '" & _thisCompType & "' "

        If thisPayrollClass <> "" Then
            _where += "AND payroll_classification = '" & thisPayrollClass & "' "
        End If

        Dim splitCriteria As String()

        splitCriteria = Split(_criteria, "zxz")

        If splitCriteria.Length > 0 Then
            If splitCriteria(0) <> "" Then
                _where += "AND payroll_mode = '" & splitCriteria(0) & "' "
            End If
            If splitCriteria(1) = "Y" Then

                If _thisCompType = "DIFFERENTIAL" Then
                    If splitCriteria(2) And splitCriteria(3) Then
                        _where += "AND payroll_from <= '" & splitCriteria(2) & "' AND  payroll_to <= '" & splitCriteria(3) & "' "
                    End If
                End If

                If splitCriteria(2) Then
                    _where += "AND payroll_month = '" & splitCriteria(2) & "' "
                End If
                If splitCriteria(3) Then
                    _where += "AND payroll_year = '" & splitCriteria(3) & "' "
                End If
            End If
        End If

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted FROM tbl_payroll_transaction_header " & _
              _where

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")
    End Function

    Public Function browsePayrollTransactionHeaderMultiple(ByVal _criteria As String, thisPayrollClass As String, _thisCompType As String, ByVal _thisDepartment As String) As DataTable

        Dim sql As String = ""

        Dim _where As String = " WHERE payroll_compensation_type = '" & _thisCompType & "' AND is_multiple='Y' AND department = '" & _thisDepartment & "' "

        If thisPayrollClass <> "" Then

            _where += "AND payroll_classification = '" & thisPayrollClass & "' "

        End If

        Dim splitCriteria As String()

        splitCriteria = Split(_criteria, "zxz")

        If splitCriteria.Length > 0 Then

            If splitCriteria(0) <> "" Then
                _where += "AND (reference_code LIKE '" & splitCriteria(0) & "%' OR payroll_details LIKE '" & splitCriteria(0) & "%')"
            End If

            If splitCriteria(1) <> "" Then
                _where += "AND payroll_compensation = '" & splitCriteria(1) & "' "
            End If

            If splitCriteria(2) = "Y" Then

                If splitCriteria(3) <> "" And splitCriteria(4) <> "" Then
                    _where += "AND payroll_from BETWEEN '" & splitCriteria(3) & "' AND '" & splitCriteria(4) & "' "
                End If

            End If

        End If

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted FROM tbl_payroll_transaction_header " & _
              _where & " ORDER BY payroll_to DESC"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")
    End Function

    Public Function browsePayrollTransactionHeaderMultipleJO(ByVal _criteria As String, thisPayrollClass As String, _thisCompType As String, ByVal _thisDepartment As String) As DataTable

        Dim sql As String = ""

        Dim _where As String = " WHERE payroll_compensation_type = '" & _thisCompType & "' AND is_multiple='Y' AND department = '" & _thisDepartment & "' AND payroll_classification = 'JOB ORDER' "

        If thisPayrollClass <> "" Then

            _where += "AND payroll_classification = '" & thisPayrollClass & "' "

        End If

        Dim splitCriteria As String()

        splitCriteria = Split(_criteria, "zxz")

        If splitCriteria.Length > 0 Then

            If splitCriteria(0) <> "" Then
                _where += "AND reference_code LIKE '" & splitCriteria(0) & "%' "
            End If

            If splitCriteria(1) <> "" Then
                _where += "AND payroll_compensation = '" & splitCriteria(1) & "' "
            End If

            If splitCriteria(2) = "Y" Then

                If splitCriteria(3) <> "" And splitCriteria(4) <> "" Then
                    _where += "AND payroll_from <= '" & splitCriteria(3) & "' AND  payroll_to <= '" & splitCriteria(4) & "' "
                End If

            End If

        End If


        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted FROM tbl_payroll_transaction_header " & _
              _where & " ORDER BY payroll_to DESC"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")
    End Function

    Public Function browsePayrollTransactionHeader(ByVal _criteria As String, thisPayrollClass As String, ByVal _thisStatus As String, Optional _thisSearch As String = "") As DataTable

        Dim sql As String = ""
        Dim _where As String = " WHERE payroll_classification='" & thisPayrollClass & "' AND payroll_compensation_type = 'SALARY' "

        If _thisStatus <> "" Then

            If _thisStatus = "BOOKED" Then
                _where += "AND payroll_status IN ('BOOKED','FOR ADA','SUBMIT TO BANK') "
            Else
                _where += "AND payroll_status = '" & _thisStatus & "' "
            End If

        End If

        If _thisSearch <> "" Then

            _where += "AND (reference_code LIKE '%" & _thisSearch & "%' OR payroll_details LIKE '%" & _thisSearch & "%')  "

        End If

        Dim splitCriteria() As String = Split(_criteria, "zxz")

        If splitCriteria.Length > 0 Then

            If splitCriteria(0) <> "" Then
                _where += " AND payroll_mode = '" & splitCriteria(0) & "' "
            End If

        End If

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted FROM tbl_payroll_transaction_header " & _
              _where & " ORDER BY payroll_to DESC"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")

    End Function

    Public Function browsePayrollTransactionHeaderStatusMultiple(ByVal _criteria As String, thisPayrollClass As String, ByVal _thisCompType As String, ByVal _thisStatus As String, Optional _thisSearch As String = "", Optional _payrollCom As String = "") As DataTable

        Dim sql As String = ""
        Dim _where As String = " WHERE payroll_classification='" & thisPayrollClass & "' AND payroll_compensation_type = '" & _thisCompType & "' AND is_multiple = 'Y' "

        If _thisStatus <> "" Then

            If _thisStatus = "BOOKED" Then
                _where += "AND payroll_status IN ('BOOKED','FOR ADA','SUBMIT TO BANK') "
            Else
                _where += "AND payroll_status = '" & _thisStatus & "' "
            End If

        End If

        Dim splitCriteria As String()

        splitCriteria = Split(_criteria, "zxz")

        If splitCriteria.Length > 0 Then

            If splitCriteria(1) = "Y" Then
                If splitCriteria(2) <> "" And splitCriteria(3) <> "" Then
                    _where += "AND payroll_from BETWEEN '" & splitCriteria(2) & "' AND '" & splitCriteria(3) & "' "
                End If
            End If

        End If

        If _payrollCom <> "" Then
            _where += "AND payroll_compensation = '" & _payrollCom & "' "
        End If

        If _thisSearch <> "" Then

            _where += "AND (reference_code LIKE '" & _thisSearch & "%' OR payroll_details LIKE '" & _thisSearch & "%') "
        End If

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted FROM tbl_payroll_transaction_header " & _
              _where & " ORDER BY payroll_from DESC "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")

    End Function

    Public Function browsePayrollTransactionHeaderOIClassification(ByVal _criteria As String, thisPayrollCompensation As String, thisClassification As String, ByVal _thisCompensation As String, ByVal _thisOffice As String) As DataTable

        Dim sql As String = ""
        Dim _where As String = " WHERE payroll_compensation_type ='" & thisPayrollCompensation & "' AND payroll_classification IN (" & thisClassification & ") AND payroll_compensation <> 'EXTRA SERVICE' "


        Dim splitCriteria As String()

        splitCriteria = Split(_criteria, "zxz")

        If splitCriteria.Length > 0 Then
            If splitCriteria(0) <> "" Then
                _where += "AND (reference_code LIKE '" & splitCriteria(0).Trim & "%' OR payroll_details LIKE '" & splitCriteria(0) & "%') "
            End If

            If splitCriteria.Length > 2 Then
                If splitCriteria(2) = "Y" Then

                    If splitCriteria(3) <> "" And splitCriteria(4) <> "" Then
                        _where += "AND payroll_month= '" & splitCriteria(3) & "' AND  payroll_year = '" & splitCriteria(4) & "' "
                    End If

                End If

            End If

        End If

        If _thisCompensation <> "" Then
            _where += "AND payroll_compensation = '" & _thisCompensation & "' "
        End If

        If _thisOffice <> "" Then
            _where += "AND department = '" & _thisOffice & "' "
        End If

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count, is_active, is_posted FROM tbl_payroll_transaction_header " & _
              _where & " ORDER BY payroll_to DESC"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")
    End Function

    Public Function browsePayrollTransactionHeaderOI(ByVal _criteria As String, ByVal thisPayrollCompensation As String, ByVal _thisStatus As String, Optional _thisClassification As String = "", Optional _thisCompensation As String = "") As DataTable

        Dim sql As String = ""
        Dim _where As String = " WHERE payroll_compensation_type IN (" & thisPayrollCompensation & ") "


        Dim splitCriteria As String()

        splitCriteria = Split(_criteria, "zxz")

        If splitCriteria.Length > 0 Then
            If splitCriteria.Length > 2 Then
                If splitCriteria(2) = "Y" Then
                    If splitCriteria(3) <> "" And splitCriteria(4) <> "" Then
                        _where += "AND payroll_month = '" & splitCriteria(3) & "' AND payroll_year =  '" & splitCriteria(4) & "' "
                    End If
                End If
            End If
            If splitCriteria(0) <> "" Then
                _where += "AND (reference_code LIKE '%" & splitCriteria(0) & "%' OR payroll_details LIKE '%" & splitCriteria(0) & "%' OR cad_no LIKE '%" & splitCriteria(0) & "%') "
            End If

        End If

        If _thisStatus <> "" Then

            If _thisStatus = "BOOKED" Then
                _where += "AND payroll_status IN ('BOOKED','FOR ADA','SUBMIT TO BANK') "
            Else
                _where += "AND payroll_status = '" & _thisStatus & "' "
            End If

        End If

        If _thisClassification <> "" Then

            _where += "AND payroll_classification IN (" & _thisClassification & ") "

        End If

        If _thisCompensation <> "" Then
            _where += "AND payroll_compensation = '" & _thisCompensation & "' "
        End If

        sql = "SELECT transaction_header_id, reference_code, payroll_classification, payroll_compensation, department, payroll_mode, payroll_month, payroll_year, " & _
              "payroll_from, payroll_to, payroll_period, payroll_details, employee_count, payroll_gross_amount, payroll_net_amount, payroll_status, transmittal_date, " & _
              "transmittal_remarks, process_count, posting_count,tbl_payroll_transaction_header.is_active, is_posted, income_desc FROM tbl_payroll_transaction_header " & _
              "INNER JOIN tbl_payroll_ref_income ON tbl_payroll_transaction_header.payroll_compensation = tbl_payroll_ref_income.income_id " & _
              _where

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")
    End Function

    Public Sub savePayrollTransactionHeader()

        If transactionHeaderId = "" Then

            With _clsDB.dbUtility
                .fieldItems = "transaction_header_id,reference_code,payroll_classification,payroll_compensation,payroll_compensation_type,payroll_fund_source,department,payroll_mode,payroll_month,payroll_year,payroll_from,payroll_to,payroll_period,payroll_details,employee_count,payroll_gross_amount,payroll_net_amount,payroll_status,process_count,posting_count,is_active,is_posted,is_multiple,payroll_remarks,create_user"
                .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "INSERT")
                _transactionHeaderId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
                .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
                .ADDPARAM_CMD_String("reference_code", Left(Guid.NewGuid().ToString.Replace("-", ""), 7).ToUpper)
                .ADDPARAM_CMD_String("payroll_classification", _payrollClassification)
                .ADDPARAM_CMD_String("payroll_compensation", _payrollCompensation)
                .ADDPARAM_CMD_String("payroll_compensation_type", _payrollCompensationType)
                .ADDPARAM_CMD_String("payroll_fund_source", _payrollFundSource)
                .ADDPARAM_CMD_String("department", _department)
                .ADDPARAM_CMD_String("payroll_mode", _payrollMode)
                .ADDPARAM_CMD_String("payroll_month", _payrollMonth)
                .ADDPARAM_CMD_String("payroll_year", _payrollYear)
                .ADDPARAM_CMD_String("payroll_from", _payrollFrom)
                .ADDPARAM_CMD_String("payroll_to", _payrollTo)
                .ADDPARAM_CMD_String("payroll_period", _payrollPeriod)
                .ADDPARAM_CMD_String("payroll_details", _payrollDetails)
                .ADDPARAM_CMD_String("employee_count", _employeeCount)
                .ADDPARAM_CMD_String("payroll_gross_amount", _payrollGrossAmount)
                .ADDPARAM_CMD_String("payroll_net_amount", _payrollNetAmount)
                .ADDPARAM_CMD_String("payroll_status", _payrollStatus)
                '.ADDPARAM_CMD_String("transmittal_date", _transmittalDate)
                '.ADDPARAM_CMD_String("transmittal_remarks", _transmittalRemarks)
                .ADDPARAM_CMD_String("process_count", _processCount)
                .ADDPARAM_CMD_String("posting_count", _postingCount)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("is_posted", _isPosted)
                .ADDPARAM_CMD_String("is_multiple", _isMultiple)
                .ADDPARAM_CMD_String("payroll_remarks", _payrollRemarks)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .executeUsingCommandFromSQL(True)
            End With

        Else

            With _clsDB.dbUtility
                .fieldItems = "payroll_fund_source,payroll_remarks"
                .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
                .ADDPARAM_CMD_String("payroll_fund_source", _payrollFundSource)
                .ADDPARAM_CMD_String("payroll_remarks", _payrollRemarks)
                .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
                .executeUsingCommandFromSQL(True)
            End With

        End If

    End Sub

    Public Sub updatePayrollTransactionHeader()

        With _clsDB.dbUtility
            .fieldItems = "payroll_fund_source,payroll_remarks,payroll_month,payroll_year,payroll_from,payroll_to,payroll_period"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("payroll_fund_source", _payrollFundSource)
            .ADDPARAM_CMD_String("payroll_remarks", _payrollRemarks)
            .ADDPARAM_CMD_String("payroll_month", _payrollMonth)
            .ADDPARAM_CMD_String("payroll_year", _payrollYear)
            .ADDPARAM_CMD_String("payroll_from", _payrollFrom)
            .ADDPARAM_CMD_String("payroll_to", _payrollTo)
            .ADDPARAM_CMD_String("payroll_period", _payrollPeriod)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollTransactionHeaderSummary()

        Dim dt As New DataTable

        Dim sql As String = "SELECT  COUNT(transaction_employees_id) AS employee_count, SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount FROM tbl_payroll_transaction_employees WHERE transaction_header_id='" & _transactionHeaderId & "' AND is_active = 'Y'"

        dt = _clsDB.Fill_DataTable(sql)

        If CInt(dt.Rows(0)("employee_count").ToString) > 0 Then

            _employeeCount = dt.Rows(0)("employee_count")
            _payrollDetails = _clsDB.Get_DB_Item("SELECT employee_name FROM tbl_payroll_transaction_employees  WHERE transaction_header_id='" & _transactionHeaderId & "' AND is_active = 'Y' ORDER BY sort_order ASC LIMIT 1 ")
            If CInt(_employeeCount) > 1 Then

                _payrollDetails = _payrollDetails & " ET. AL."

            End If

            _payrollGrossAmount = dt.Rows(0)("gross_amount")
            _payrollNetAmount = dt.Rows(0)("net_amount")

        End If

        With _clsDB.dbUtility
            .fieldItems = "payroll_details,employee_count,payroll_gross_amount,payroll_net_amount,process_count"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("payroll_details", _payrollDetails)
            .ADDPARAM_CMD_String("employee_count", _employeeCount)
            .ADDPARAM_CMD_String("payroll_gross_amount", _payrollGrossAmount)
            .ADDPARAM_CMD_String("payroll_net_amount", _payrollNetAmount)
            .ADDPARAM_CMD_String("process_count", _processCount)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollTransactionHeaderSummaryMultiple()

        Dim dt As New DataTable

        Dim sql As String = "SELECT  COUNT(DISTINCT employee_id) AS employee_count, SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount FROM tbl_payroll_transaction_employees WHERE transaction_header_id='" & _transactionHeaderId & "'"

        dt = _clsDB.Fill_DataTable(sql)

        If CInt(dt.Rows(0)("employee_count").ToString) > 0 Then

            _employeeCount = dt.Rows(0)("employee_count")
            _payrollDetails = _clsDB.Get_DB_Item("SELECT employee_name FROM tbl_payroll_transaction_employees  WHERE transaction_header_id='" & _transactionHeaderId & "' ORDER BY sort_order ASC ")
            If CInt(_employeeCount) > 1 Then

                _payrollDetails = _payrollDetails & " ET. AL."

            End If

            _payrollGrossAmount = dt.Rows(0)("gross_amount")
            _payrollNetAmount = dt.Rows(0)("net_amount")

            Dim dtPeriod As New DataTable

            sql = "SELECT DATE_FORMAT(MIN(emp_payroll_from),'%M %d, %Y') AS emp_payroll_from, DATE_FORMAT(MAX(emp_payroll_to),'%M %d, %Y') AS emp_payroll_to FROM tbl_payroll_transaction_employees " & _
                  "WHERE transaction_header_id = '" & _transactionHeaderId & "' AND is_initial = 'N' LIMIT 1 "

            dtPeriod = _clsDB.Fill_DataTable(sql)

            If dtPeriod.Rows.Count > 0 Then

                _payrollPeriod = dtPeriod.Rows(0)("emp_payroll_from") & " - " & dtPeriod.Rows(0)("emp_payroll_to")

                Try
                    _payrollFrom = CDate(dtPeriod.Rows(0)("emp_payroll_from")).ToString("yyyy-MM-dd")
                    _payrollTo = CDate(dtPeriod.Rows(0)("emp_payroll_to")).ToString("yyyy-MM-dd")
                Catch ex As Exception
                    _payrollFrom = "0000-00-00"
                    _payrollTo = "0000-00-00"
                End Try

            End If

        End If

        With _clsDB.dbUtility
            .fieldItems = "payroll_details,payroll_from,payroll_to,employee_count,payroll_gross_amount,payroll_net_amount,process_count,payroll_period"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("payroll_details", _payrollDetails)
            .ADDPARAM_CMD_String("payroll_from", _payrollFrom)
            .ADDPARAM_CMD_String("payroll_to", _payrollTo)
            .ADDPARAM_CMD_String("employee_count", _employeeCount)
            .ADDPARAM_CMD_String("payroll_gross_amount", _payrollGrossAmount)
            .ADDPARAM_CMD_String("payroll_net_amount", _payrollNetAmount)
            .ADDPARAM_CMD_String("process_count", _processCount)
            .ADDPARAM_CMD_String("payroll_period", _payrollPeriod)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollTransactionHeaderPayrollDetails()

        Dim dt As New DataTable

        Dim sql As String = "SELECT COUNT(transaction_employees_id) AS employee_count, SUM(gross_amount) AS gross_amount, SUM(net_amount) AS net_amount FROM tbl_payroll_transaction_employees WHERE transaction_header_id='" & _transactionHeaderId & "' ORDER BY sort_order ASC LIMIT 1"

        dt = _clsDB.Fill_DataTable(sql)

        If dt.Rows.Count > 0 Then
            _employeeCount = dt.Rows(0)("employee_count")
            _payrollDetails = _clsDB.Get_DB_Item("SELECT employee_name FROM tbl_payroll_transaction_employees  WHERE transaction_header_id='" & _transactionHeaderId & "' ORDER BY sort_order ASC ")

            If CInt(_employeeCount) > 1 Then
                _payrollDetails = _payrollDetails & " ET. AL."
            End If


        End If

        With _clsDB.dbUtility
            .fieldItems = "payroll_details"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("payroll_details", _payrollDetails)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollTransactionHeaderDetails()
        With _clsDB.dbUtility
            .fieldItems = "payroll_details"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("payroll_details", _payrollDetails)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionHeaderPayrollStatus()
        With _clsDB.dbUtility
            .fieldItems = "payroll_status"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("payroll_status", _payrollStatus)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub


    Public Sub updatePayrollTransactionHeaderAudited()
        With _clsDB.dbUtility
            .fieldItems = "audited_date"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("audited_date", _auditedDate)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updatePayrollTransactionHeaderPayrollBook()
        With _clsDB.dbUtility
            .fieldItems = "booked_date"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("booked_date", _bookedDate)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub


    Public Sub updatePayrollCadNo()
        With _clsDB.dbUtility
            .fieldItems = "cad_no"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("cad_no", cadNo)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updateVoucherDesc()
        With _clsDB.dbUtility
            .fieldItems = "voucher_desc"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_header", "UPDATE", "transaction_header_id")
            .ADDPARAM_CMD_String("voucher_desc", _voucherDesc)
            .ADDPARAM_CMD_String("transaction_header_id", _transactionHeaderId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub getPayrollTransactionHeader(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_transaction_header WHERE transaction_header_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _transactionHeaderId = dt.Rows(0)("transaction_header_id").ToString
            _referenceCode = dt.Rows(0)("reference_code").ToString
            _cadNo = dt.Rows(0)("cad_no").ToString
            _payrollClassification = dt.Rows(0)("payroll_classification").ToString
            _payrollCompensation = dt.Rows(0)("payroll_compensation").ToString
            _payrollCompensationType = dt.Rows(0)("payroll_compensation_type").ToString
            _payrollFundSource = dt.Rows(0)("payroll_fund_source").ToString
            _department = dt.Rows(0)("department").ToString
            _payrollMode = dt.Rows(0)("payroll_mode").ToString
            _payrollMonth = dt.Rows(0)("payroll_month").ToString
            _payrollYear = dt.Rows(0)("payroll_year").ToString
            _payrollFrom = dt.Rows(0)("payroll_from").ToString
            _payrollTo = dt.Rows(0)("payroll_to").ToString
            _payrollPeriod = dt.Rows(0)("payroll_period").ToString
            _payrollDetails = dt.Rows(0)("payroll_details").ToString
            _employeeCount = dt.Rows(0)("employee_count").ToString
            _payrollGrossAmount = dt.Rows(0)("payroll_gross_amount").ToString
            _payrollNetAmount = dt.Rows(0)("payroll_net_amount").ToString
            _payrollStatus = dt.Rows(0)("payroll_status").ToString
            _transmittalDate = dt.Rows(0)("transmittal_date").ToString
            _transmittalRemarks = dt.Rows(0)("transmittal_remarks").ToString
            _processCount = dt.Rows(0)("process_count").ToString
            _postingCount = dt.Rows(0)("posting_count").ToString
            _isActive = dt.Rows(0)("is_active").ToString
            _isPosted = dt.Rows(0)("is_posted").ToString
            _payrollRemarks = dt.Rows(0)("payroll_remarks").ToString
            _isMultiple = dt.Rows(0)("is_multiple").ToString
            _voucherDesc = dt.Rows(0)("voucher_desc").ToString
        Else
            initialize()
        End If
    End Sub

    Public Sub deletePayrollTransactionHeader(ByVal _id As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_transaction_header WHERE transaction_header_id='" & _id & "' LIMIT 1")
    End Sub

End Class
