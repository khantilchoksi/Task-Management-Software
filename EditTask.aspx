<%@ Page Title="Edit Task" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditTask.aspx.cs" Inherits="EditTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!-- bootstrap datepicker -->
  <link rel="stylesheet" href="plugins/datepicker/datepicker3.css">

  <!-- Validator CSS-->
  <link rel="stylesheet" href="plugins/validation/dist/css/bootstrapValidator.min.css" />

  <!-- Bootstrap time Picker -->
  <link rel="stylesheet" href="plugins/timepicker/bootstrap-timepicker.min.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Edit Task
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="Dashboard.aspx">Dashboard</a></li>
        <li><a href="Tasks.aspx">Tasks</a></li>
        <li class="active">Edit Task</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <!-- left column -->
        <div class="col-md-12">
          <!-- general form elements -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Task Details</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <!--form role="form"-->
              <div class="box-body">
                <div class="form-group">
                  <label for="exampleInputEmail1">Task Title</label>
                  <!--input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email"-->
                  <!--input class="form-control input-lg" type="text" placeholder=".input-lg"-->
                  <asp:TextBox ID="TaskTitleTextBox" runat="server" CssClass="form-control input-lg" placeholder="Task Title"></asp:TextBox>
                </div>


                <div class="form-group col-xs-6">
                  <label for="exampleInputPassword1">Project</label>
                  <!--input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password"-->
                  <!-- select -->

                  <asp:TextBox ID="ProjectTextBox" runat="server" CssClass="form-control input-lg" placeholder="Project name" ReadOnly="True" Height="34px" Font-Size="15px"></asp:TextBox>
<%--
                  <asp:DropDownList ID="ProjectDropDownList" runat="server"  CssClass="form-control" DataSourceID="SqlDataSource1" 
                        DataTextField="project_title" DataValueField="project_id" AutoPostBack="true"
                        onselectedindexchanged="ProjectDropDownList_SelectedIndexChanged" AppendDataBoundItems="True">--%>
                        <%--<asp:ListItem Text="Select a project" Value="-1" />--%>
<%--                        </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:testcon %>" 
                        SelectCommand="SELECT [project_id], [project_title] FROM [projects] WHERE ([created_by] = @created_by) ORDER BY [project_title]">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="1" Name="created_by" SessionField="user" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>

                </div>
                


                <div class="form-group col-xs-3">
                                  <%--<input type="file" id="exampleInputFile">

                  <p class="help-block">Example block-level help text here.</p>--%>
                  <label for="dueDate">Due Date</label>

                  <div class="input-group date">
                  <div class="input-group-addon">
                    <i class="fa fa-calendar"></i>
                  </div>
                    <asp:TextBox ID="datepicker" runat="server" class="form-control pull-right"></asp:TextBox>
                  <!--input type="text"  id="datepicker"-->
                </div>

                </div>

                <!-- time Picker -->
              <div class="form-group col-xs-3">
              <div class="bootstrap-timepicker">
                
                  <label>Due Time</label>

                  <div class="input-group">
                    <%--<input type="text" class="form-control timepicker">--%>
                    <asp:TextBox ID="timePicker" runat="server" class="form-control timepicker"></asp:TextBox>

                    <div class="input-group-addon">
                      <i class="fa fa-clock-o"></i>
                    </div>
                  </div>
                  <!-- /.input group -->
                </div>
                <!-- /.form group -->
              </div>

                <!-- In order to make the website responsive with errors, following division is placed -->
                <div class="form-group col-xs-12"></div>

                <div class="form-group col-xs-6">
                  <label for="exampleInputPassword1">Status</label>

                  <asp:DropDownList ID="TaskStatusDropDownList" runat="server" 
                        CssClass="form-control" DataSourceID="SqlDataSource2" 
                        DataTextField="status_name" DataValueField="status_id" ></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:testcon %>" 
                        SelectCommand="SELECT * FROM [task_status] ORDER BY [status_id]">
                    </asp:SqlDataSource>
                </div>

                <div class="form-group col-xs-6">
                  <label for="exampleInputPassword1">Priority</label>

                   <asp:DropDownList ID="TaskPriorityDropDownList" runat="server" CssClass="form-control" DataSourceID="SqlDataSource3" 
                        DataTextField="priority_name" DataValueField="priority_id"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:testcon %>" 
                        SelectCommand="SELECT * FROM [task_priority] ORDER BY [priority_id]">
                    </asp:SqlDataSource>

                  </div>

                  <!-- textarea -->
                <div class="form-group col-xs-12">
                  <label>Task Description</label>
                  <asp:TextBox ID="DescriptionTextBox" TextMode="multiline" Columns="50" Rows="3" class="form-control" runat="server" MaxLength="10" placeholder="Enter task description ..."></asp:TextBox>
                  <%--<textarea class="form-control" rows="3" placeholder="Enter task description ..."></textarea>--%>
                </div>

                <div class="form-group col-xs-6">
                  <label for="exampleInputPassword1">Task Assignees</label>
                   <asp:CheckBoxList ID="AssigneeCheckboxList" runat="server" CssClass="checkbox"
                        DataTextField="Column1" DataValueField="user_id" CellPadding="5" 
                        CellSpacing="5" ondatabound="AssigneeCheckboxList_DataBound"></asp:CheckBoxList>
                 </div>

              </div>
              <!-- /.box-body -->
              <div class="box-footer">
                <!--button type="submit" class="btn btn-default">Cancel</button-->
                <a href="Tasks.aspx" class="btn btn-default">Cancel</a>
                <!--button type="submit" class="btn btn-info pull-right">Sign in</button-->
                <asp:Button ID="SaveTaskChangesButton" runat="server" Text="Save Changes" onclick="SaveTaskChangesButton_Click" CssClass="btn btn-info pull-right"></asp:Button>
              </div>
              <!-- /.box-footer -->

            <!--/form-->
          </div>
          <!-- /.box -->

      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<!-- bootstrap datepicker -->

<script type="text/javascript">
    function DisplayConfirmation() {
        if (confirm('Are you sure you want to change the project?')) {
//            __doPostback('ProjectDropDownList', '');
        } else {
        return false;
        }
    }
</script>

<script src="plugins/datepicker/bootstrap-datepicker.js"></script>
<script>
    $(function () {
        //Date picker
        $('#ContentPlaceHolder1_datepicker').datepicker({
            autoclose: true
        });

        //time picker
        $(".timepicker").timepicker({
            showInputs: false
        });
    });
</script>
<script src="plugins/validation/dist/js/bootstrapValidator.min.js"></script>
<!-- bootstrap time picker -->
<script src="plugins/timepicker/bootstrap-timepicker.min.js"></script>
<script>
    $(document).ready(function () {
        $('#form1').bootstrapValidator({
            fields: {
                <%=TaskTitleTextBox.UniqueID%>: {
                    validators: {
                        notEmpty: {
                            message: 'Enter task title.'
                        }
                    }
                },
                <%=datepicker.UniqueID%>: {
                    validators: {
                        notEmpty: {
                            message: 'Enter task due date.'
                        }
                    }
                },
                <%=timePicker.UniqueID%>: {
                    validators: {
                        notEmpty: {
                            message: 'Enter task due time.'
                        }
                    }
                },
                
            }
        });
    });
</script>

</asp:Content>

