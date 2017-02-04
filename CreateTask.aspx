<%@ Page Title="Create Task" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateTask.aspx.cs" Inherits="CreateTaskWithMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!-- bootstrap datepicker -->
  <link rel="stylesheet" href="plugins/datepicker/datepicker3.css">

    <!-- daterange picker -->
  <link rel="stylesheet" href="plugins/daterangepicker/daterangepicker.css">

  <!-- Validator CSS-->
  <link rel="stylesheet" href="plugins/validation/dist/css/bootstrapValidator.min.css" />

    <!-- Bootstrap time Picker -->
  <link rel="stylesheet" href="plugins/timepicker/bootstrap-timepicker.min.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Create a Task
        <small>For your project</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="Dashboard.aspx">Dashboard</a></li>
        <li class="active">Create Task</li>
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

                  <asp:DropDownList ID="ProjectDropDownList" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" 
                        DataTextField="project_title" DataValueField="project_id" AutoPostBack="true"
                        onselectedindexchanged="ProjectDropDownList_SelectedIndexChanged" AppendDataBoundItems="True">
                        <asp:ListItem Text="Select a project" Value="-1" />
                        </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:testcon %>" 
                        
                        SelectCommand="select p.project_id, project_title from projects p, project_network pn where p.project_id =  pn.project_id and member_id = @memberid and p.deleted_at is null order by project_title">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="memberid" SessionField="user" />
                        </SelectParameters>
                    </asp:SqlDataSource>
<%--
                  <select class="form-control">
                    <option>option 1</option>
                    <option>option 2</option>
                    <option>option 3</option>
                    <option>option 4</option>
                    <option>option 5</option>
                  </select>--%>
                </div>
                
<%--                <div class="col-xs-2">
                </div>--%>

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

<%--                <!-- Date and time range -->
              <div class="form-group col-xs-6">
                <label>Date and time range:</label>

                <div class="input-group">
                  <div class="input-group-addon">
                    <i class="fa fa-clock-o"></i>
                  </div>
                  <input type="text" class="form-control pull-right" id="reservationtime">
                  <asp:TextBox ID="dateAndTimePicker" runat="server" class="form-control pull-right"></asp:TextBox>
                </div>
                <!-- /.input group -->
              </div>--%>

                <!-- In order to make the website responsive with errors, following division is placed -->
                <div class="form-group col-xs-12"></div>

                <div class="form-group col-xs-6">
                  <label for="exampleInputPassword1">Status</label>
                  <asp:DropDownList ID="TaskStatusDropDownList" runat="server" CssClass="form-control" DataSourceID="SqlDataSource2" 
                        DataTextField="status_name" DataValueField="status_id"></asp:DropDownList>
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
                        CellSpacing="5"></asp:CheckBoxList>
                 </div>
<%--
                <div class="checkbox">
                  <label>
                    <input type="checkbox"> Notify Assignees
                  </label>
                </div>--%>

              </div>
              <!-- /.box-body -->

              <div class="box-footer">
                <asp:Button ID="SubmitButton" runat="server" CssClass="btn btn-primary" 
                      Text="Submit" onclick="SubmitButton_Click"></asp:Button>
                <!--button type="submit" class="btn btn-primary">Submit</button-->
              </div>
            <!--/form-->
          </div>
          <!-- /.box -->

<%--          <!-- Form Element sizes -->
          <div class="box box-success">
            <div class="box-header with-border">
              <h3 class="box-title">Different Height</h3>
            </div>
            <div class="box-body">
              <input class="form-control input-lg" type="text" placeholder=".input-lg">
              <br>
              <input class="form-control" type="text" placeholder="Default input">
              <br>
              <input class="form-control input-sm" type="text" placeholder=".input-sm">
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

          <div class="box box-danger">
            <div class="box-header with-border">
              <h3 class="box-title">Different Width</h3>
            </div>
            <div class="box-body">
              <div class="row">
                <div class="col-xs-3">
                  <input type="text" class="form-control" placeholder=".col-xs-3">
                </div>
                <div class="col-xs-4">
                  <input type="text" class="form-control" placeholder=".col-xs-4">
                </div>
                <div class="col-xs-5">
                  <input type="text" class="form-control" placeholder=".col-xs-5">
                </div>
              </div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

          <!-- Input addon -->
          <div class="box box-info">
            <div class="box-header with-border">
              <h3 class="box-title">Input Addon</h3>
            </div>
            <div class="box-body">
              <div class="input-group">
                <span class="input-group-addon">@</span>
                <input type="text" class="form-control" placeholder="Username">
              </div>
              <br>

              <div class="input-group">
                <input type="text" class="form-control">
                <span class="input-group-addon">.00</span>
              </div>
              <br>

              <div class="input-group">
                <span class="input-group-addon">$</span>
                <input type="text" class="form-control">
                <span class="input-group-addon">.00</span>
              </div>

              <h4>With icons</h4>

              <div class="input-group">
                <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                <input type="email" class="form-control" placeholder="Email">
              </div>
              <br>

              <div class="input-group">
                <input type="text" class="form-control">
                <span class="input-group-addon"><i class="fa fa-check"></i></span>
              </div>
              <br>

              <div class="input-group">
                <span class="input-group-addon"><i class="fa fa-dollar"></i></span>
                <input type="text" class="form-control">
                <span class="input-group-addon"><i class="fa fa-ambulance"></i></span>
              </div>

              <h4>With checkbox and radio inputs</h4>

              <div class="row">
                <div class="col-lg-6">
                  <div class="input-group">
                        <span class="input-group-addon">
                          <input type="checkbox">
                        </span>
                    <input type="text" class="form-control">
                  </div>
                  <!-- /input-group -->
                </div>
                <!-- /.col-lg-6 -->
                <div class="col-lg-6">
                  <div class="input-group">
                        <span class="input-group-addon">
                          <input type="radio">
                        </span>
                    <input type="text" class="form-control">
                  </div>
                  <!-- /input-group -->
                </div>
                <!-- /.col-lg-6 -->
              </div>
              <!-- /.row -->

              <h4>With buttons</h4>

              <p class="margin">Large: <code>.input-group.input-group-lg</code></p>

              <div class="input-group input-group-lg">
                <div class="input-group-btn">
                  <button type="button" class="btn btn-warning dropdown-toggle" data-toggle="dropdown">Action
                    <span class="fa fa-caret-down"></span></button>
                  <ul class="dropdown-menu">
                    <li><a href="#">Action</a></li>
                    <li><a href="#">Another action</a></li>
                    <li><a href="#">Something else here</a></li>
                    <li class="divider"></li>
                    <li><a href="#">Separated link</a></li>
                  </ul>
                </div>
                <!-- /btn-group -->
                <input type="text" class="form-control">
              </div>
              <!-- /input-group -->
              <p class="margin">Normal</p>

              <div class="input-group">
                <div class="input-group-btn">
                  <button type="button" class="btn btn-danger">Action</button>
                </div>
                <!-- /btn-group -->
                <input type="text" class="form-control">
              </div>
              <!-- /input-group -->
              <p class="margin">Small <code>.input-group.input-group-sm</code></p>

              <div class="input-group input-group-sm">
                <input type="text" class="form-control">
                    <span class="input-group-btn">
                      <button type="button" class="btn btn-info btn-flat">Go!</button>
                    </span>
              </div>
              <!-- /input-group -->
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

        </div>
        <!--/.col (left) -->
        <!-- right column -->
        <div class="col-md-6">
          <!-- Horizontal Form -->
          <div class="box box-info">
            <div class="box-header with-border">
              <h3 class="box-title">Horizontal Form</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <form class="form-horizontal">
              <div class="box-body">
                <div class="form-group">
                  <label for="inputEmail3" class="col-sm-2 control-label">Email</label>

                  <div class="col-sm-10">
                    <input type="email" class="form-control" id="inputEmail3" placeholder="Email">
                  </div>
                </div>
                <div class="form-group">
                  <label for="inputPassword3" class="col-sm-2 control-label">Password</label>

                  <div class="col-sm-10">
                    <input type="password" class="form-control" id="inputPassword3" placeholder="Password">
                  </div>
                </div>
                <div class="form-group">
                  <div class="col-sm-offset-2 col-sm-10">
                    <div class="checkbox">
                      <label>
                        <input type="checkbox"> Remember me
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <!-- /.box-body -->
              <div class="box-footer">
                <button type="submit" class="btn btn-default">Cancel</button>
                <button type="submit" class="btn btn-info pull-right">Sign in</button>
              </div>
              <!-- /.box-footer -->
            </form>
          </div>
          <!-- /.box -->
          <!-- general form elements disabled -->
          <div class="box box-warning">
            <div class="box-header with-border">
              <h3 class="box-title">General Elements</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <form role="form">
                <!-- text input -->
                <div class="form-group">
                  <label>Text</label>
                  <input type="text" class="form-control" placeholder="Enter ...">
                </div>
                <div class="form-group">
                  <label>Text Disabled</label>
                  <input type="text" class="form-control" placeholder="Enter ..." disabled>
                </div>

                <!-- textarea -->
                <div class="form-group">
                  <label>Textarea</label>
                  <textarea class="form-control" rows="3" placeholder="Enter ..."></textarea>
                </div>
                <div class="form-group">
                  <label>Textarea Disabled</label>
                  <textarea class="form-control" rows="3" placeholder="Enter ..." disabled></textarea>
                </div>

                <!-- input states -->
                <div class="form-group has-success">
                  <label class="control-label" for="inputSuccess"><i class="fa fa-check"></i> Input with success</label>
                  <input type="text" class="form-control" id="inputSuccess" placeholder="Enter ...">
                  <span class="help-block">Help block with success</span>
                </div>
                <div class="form-group has-warning">
                  <label class="control-label" for="inputWarning"><i class="fa fa-bell-o"></i> Input with
                    warning</label>
                  <input type="text" class="form-control" id="inputWarning" placeholder="Enter ...">
                  <span class="help-block">Help block with warning</span>
                </div>
                <div class="form-group has-error">
                  <label class="control-label" for="inputError"><i class="fa fa-times-circle-o"></i> Input with
                    error</label>
                  <input type="text" class="form-control" id="inputError" placeholder="Enter ...">
                  <span class="help-block">Help block with error</span>
                </div>

                <!-- checkbox -->
                <div class="form-group">
                  <div class="checkbox">
                    <label>
                      <input type="checkbox">
                      Checkbox 1
                    </label>
                  </div>

                  <div class="checkbox">
                    <label>
                      <input type="checkbox">
                      Checkbox 2
                    </label>
                  </div>

                  <div class="checkbox">
                    <label>
                      <input type="checkbox" disabled>
                      Checkbox disabled
                    </label>
                  </div>
                </div>

                <!-- radio -->
                <div class="form-group">
                  <div class="radio">
                    <label>
                      <input type="radio" name="optionsRadios" id="optionsRadios1" value="option1" checked>
                      Option one is this and that&mdash;be sure to include why it's great
                    </label>
                  </div>
                  <div class="radio">
                    <label>
                      <input type="radio" name="optionsRadios" id="optionsRadios2" value="option2">
                      Option two can be something else and selecting it will deselect option one
                    </label>
                  </div>
                  <div class="radio">
                    <label>
                      <input type="radio" name="optionsRadios" id="optionsRadios3" value="option3" disabled>
                      Option three is disabled
                    </label>
                  </div>
                </div>

                <!-- select -->
                <div class="form-group">
                  <label>Select</label>
                  <select class="form-control">
                    <option>option 1</option>
                    <option>option 2</option>
                    <option>option 3</option>
                    <option>option 4</option>
                    <option>option 5</option>
                  </select>
                </div>
                <div class="form-group">
                  <label>Select Disabled</label>
                  <select class="form-control" disabled>
                    <option>option 1</option>
                    <option>option 2</option>
                    <option>option 3</option>
                    <option>option 4</option>
                    <option>option 5</option>
                  </select>
                </div>

                <!-- Select multiple-->
                <div class="form-group">
                  <label>Select Multiple</label>
                  <select multiple class="form-control">
                    <option>option 1</option>
                    <option>option 2</option>
                    <option>option 3</option>
                    <option>option 4</option>
                    <option>option 5</option>
                  </select>
                </div>
                <div class="form-group">
                  <label>Select Multiple Disabled</label>
                  <select multiple class="form-control" disabled>
                    <option>option 1</option>
                    <option>option 2</option>
                    <option>option 3</option>
                    <option>option 4</option>
                    <option>option 5</option>
                  </select>
                </div>

              </form>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
        <!--/.col (right) -->--%>

      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<!-- bootstrap datepicker -->
<script src="plugins/datepicker/bootstrap-datepicker.js"></script>
<!-- bootstrap time picker -->
<script src="plugins/timepicker/bootstrap-timepicker.min.js"></script>

    <script>
        $(function () {
            //Date picker
            $('#ContentPlaceHolder1_datepicker').datepicker({
                autoclose: true,
            });

                //Timepicker
            $(".timepicker").timepicker({
                showInputs: false
            });

            //Date range picker with time picker
            $('#ContentPlaceHolder1_dateAndTimePicker').daterangepicker({ timePicker: true, timePickerIncrement: 30, format: 'DD/MM/YYYY hh:mm:ss A' });
        });
</script>
<script src="plugins/validation/dist/js/bootstrapValidator.min.js"></script>

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
                <%=ProjectDropDownList.UniqueID%>: {
                    validators: {
                        greaterThan: {
                            value: 0,
                            message: 'Select project from the dropdown list.'
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

