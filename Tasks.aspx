<%@ Page Title="Tasks" Debug="true" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Tasks.aspx.cs" Inherits="Tasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!-- DataTables -->
    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css">
<!-- jvectormap -->
    <link rel="stylesheet" href="plugins/jvectormap/jquery-jvectormap-1.2.2.css">
    <style>
        .dataTables_wrapper 
        {
            padding:10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Tasks
        <small runat="server" id="projectSubTitle"> All Projects</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Tasks</li>
      </ol>
    </section>

          <!-- =========================================================== -->
        <!-- Sections-->
      <section class="content" style="min-height:105px">
      <div class="row">
       <asp:LinkButton ID="AllTaskLinkButton" runat="server" 
              onclick="AllTaskLinkButton_Click">
        <div class="col-md-3 col-sm-6 col-xs-12" style="width:20%">
          <div class="info-box bg-aqua">
            <span class="info-box-icon" style="width:60px"><i class="fa fa-list-ul" style="padding-top:25px; font-size:smaller"></i></span>

            <div class="info-box-content" style="margin-left:60px">
              <span class="info-box-text" style="font-size:15px">All</span>
              <span class="info-box-number" runat="server" id="allTasks" style="font-size:28px"></span>

              <%--<div class="progress">
                <div class="progress-bar" style="width: 70%"></div>
              </div>
                  <span class="progress-description">
                    70% Increase in 30 Days
                  </span>--%>
            </div>

            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        </asp:LinkButton>
        <!-- /.col -->
        <asp:LinkButton ID="ActiveTaskLinkButton" runat="server" 
              onclick="ActiveTaskLinkButton_Click">


        <div class="col-md-3 col-sm-6 col-xs-12" style="width:20%">
          <div class="info-box bg-blue">
            <span class="info-box-icon" style="width:60px"><i class="fa fa-play" style="padding-top:25px; font-size:smaller"></i></span>

            <div class="info-box-content" style="margin-left:60px">
              <span class="info-box-text" style="font-size:15px">Active</span>
              <span class="info-box-number" runat="server" id="activeTasks" style="font-size:28px"></span>

              <%--<div class="progress">
                <div class="progress-bar" style="width: 70%"></div>
              </div>
                  <span class="progress-description">
                    70% Increase in 30 Days
                  </span>--%>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        </asp:LinkButton>
        <!-- /.col -->
        <asp:LinkButton ID="CompletedTaskLinkButton" runat="server" 
              onclick="CompletedTaskLinkButton_Click">
        <div class="col-md-3 col-sm-6 col-xs-12" style="width:20%">
          <div class="info-box bg-green">
            <span class="info-box-icon" style="width:60px"><i class="fa fa-check-square-o" style="padding-top:25px; font-size:smaller"></i></span>

            <div class="info-box-content" style="margin-left:60px">
              <span class="info-box-text" style="font-size:15px">Completed</span>
              <span class="info-box-number" runat="server" id="completedTasks" style="font-size:28px"></span>

              <%--<div class="progress">
                <div class="progress-bar" style="width: 70%"></div>
              </div>
                  <span class="progress-description">
                    70% Increase in 30 Days
                  </span>--%>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        </asp:LinkButton>
        <!-- /.col -->
        <asp:LinkButton ID="DueTodayLinkButton" runat="server" 
              onclick="DueTodayLinkButton_Click">
        <div class="col-md-3 col-sm-6 col-xs-12" style="width:20%">
          <div class="info-box bg-yellow">
            <span class="info-box-icon" style="width:60px"><i class="fa fa-calendar" style="padding-top:25px; font-size:smaller"></i></span>

            <div class="info-box-content" style="margin-left:60px">
              <span class="info-box-text" style="font-size:15px">Due Today</span>
              <span class="info-box-number" runat="server" id="dueTodayTasks" style="font-size:28px"></span>

              <%--<div class="progress">
                <div class="progress-bar" style="width: 70%"></div>
              </div>
                  <span class="progress-description">
                    70% Increase in 30 Days
                  </span>--%>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
          </div>
          </asp:LinkButton>
        <!-- /.col -->
        <asp:LinkButton ID="LateLinkButton" runat="server" onclick="LateLinkButton_Click">
        <div class="col-md-3 col-sm-6 col-xs-12" style="width:20%">
          <div class="info-box bg-red">
            <span class="info-box-icon" style="width:60px"><i class="fa fa-exclamation" style="padding-top:25px; font-size:smaller"></i></span>

            <div class="info-box-content" style="margin-left:60px">
              <span class="info-box-text" style="font-size:15px">Late</span>
              <span class="info-box-number" runat="server" id="lateTasks" style="font-size:28px"></span>

              <%--div class="progress">
                <div class="progress-bar" style="width: 70%"></div>
              </div>
                  <span class="progress-description">
                    70% Increase in 30 Days
                  </span>--%>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        </asp:LinkButton>
        <!-- /.col -->
      </div>
      </section>

      <!-- End of Sections-->

    <!-- Task Details -->
    <section class="content">
<%--          <div class="row">
          <div class="col-xs-12">
                <a href="CreateTask.aspx" class="btn btn-primary"><i class="fa fa-plus"></i> Add New Task</a>
                </div>
      </div>--%>
      <!-- Info boxes -->
<%--      <div class="row">
        <div class="col-md-3 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-aqua"><i class="ion ion-ios-gear-outline"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">CPU Traffic</span>
              <span class="info-box-number">90<small>%</small></span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-3 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-red"><i class="fa fa-google-plus"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Likes</span>
              <span class="info-box-number">41,410</span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->

        <!-- fix for small devices only -->
        <div class="clearfix visible-sm-block"></div>

        <div class="col-md-3 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-green"><i class="ion ion-ios-cart-outline"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Sales</span>
              <span class="info-box-number">760</span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-3 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">New Members</span>
              <span class="info-box-number">2,000</span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
      </div>--%>
      <!-- /.row -->

       <div class="row">
        <div class="col-xs-12">
          
          <!-- /.box -->
          <div class="box">
          <!--br />
          <div class="col-xs-12">
          <a href="AddProject.aspx" class="btn btn-primary" style="float:left";>Add New</a>
          </div-->
            <div class="box-header">

              <i class="fa fa-list-ul" runat="server" id="dataTableSymbol"></i><h3 class="box-title" runat="server" id="dataTableTitle"> All Tasks</h3>
              <a href="CreateTask.aspx" class="btn btn-primary" style="margin-left:30px; float:right"><i class="fa fa-plus"></i> Add New Task</a>
              <div style="width:35%;float:right; text-align:center">
              <h3 class="box-title" style="padding-top:8px"> Project</h3>
              
              <%--<label for="exampleInputPassword1">Project:  </label>--%>
                      <asp:DropDownList ID="ProjectDropDownList" runat="server" Width="70%" CssClass="form-control" DataSourceID="SqlDataSourceProj" 
                        DataTextField="project_title" DataValueField="project_id" AutoPostBack="true"
                        onselectedindexchanged="ProjectDropDownList_SelectedIndexChanged" AppendDataBoundItems="True">
                        <asp:ListItem Text="All Projects" Value="-1" />
                        </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceProj" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:testcon %>" 
                        
                        SelectCommand="select p.project_id, project_title from projects p, project_network pn where p.project_id =  pn.project_id and member_id = @memberid order by project_title">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="memberid" SessionField="user" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>

            </div>
            <!-- /.box-header -->
            <div runat="server" id="NoTasksDiv" class="box-body table-responsive" style="overflow-x:visible"></div>
            
            <asp:GridView ID="TaskGridView" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-striped table-bordered" 
                    onrowdatabound="TaskGridView_RowDataBound" 
                    onrowcreated="TaskGridView_RowCreated"                     
                    DataKeyNames="task_id">


                <Columns>

<%--                   <asp:TemplateField HeaderText="Task Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="statusDropdownList" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

<%--                    //0--%>
                    <asp:BoundField DataField="task_id" HeaderText="task_id" 
                        SortExpression="task_id" Visible="False" />
<%--                    //1--%>
                    <asp:TemplateField HeaderText="Status" SortExpression="task_status" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="taskStatusLabel" runat="server" Text='<%# Bind("task_status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

<%--                    //2--%>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="statusDropdownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="stausDropdownList_OnSelectedIndexChanged"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

<%--                    //3--%>
                    <asp:TemplateField HeaderText="Title" SortExpression="task_title">
                        <ItemTemplate>
                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("task_title") %>'></asp:Label>--%>
                            <asp:LinkButton ID="viewTaskLinkButton" runat="server" Text='<%# Bind("task_title") %>' OnClick="viewTask" CommandArgument='<%# Bind("task_id") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

<%--                    //4--%>
                    <asp:TemplateField HeaderText="Project" SortExpression="project_id">
                        <ItemTemplate>
                            <%--<asp:Label ID="Label2" runat="server" Text='<%# Bind("project_id") %>'></asp:Label>--%>
                            <asp:LinkButton ID="viewProjectLinkButton" runat="server" Text='<%# Bind("project_id") %>' OnClick="viewProject" CommandArgument='<%# Bind("project_id") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

<%--                    //5--%>
                    <asp:TemplateField HeaderText="Priority" SortExpression="task_priority" 
                        Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="taskPriorityLabel" runat="server" Text='<%# Bind("task_priority") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

<%--                    //6--%>
                    <asp:TemplateField HeaderText="Priority">
                        <ItemTemplate>
                            <asp:DropDownList ID="priorityDropdownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="priorityDropdownList_OnSelectedIndexChanged"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

<%--                    //7--%>
                    <asp:BoundField DataField="due_date" HeaderText="Due Date" 
                        SortExpression="due_date" />
<%--                    //8--%>
                    <asp:BoundField DataField="created_at" HeaderText="Created On" 
                        SortExpression="created_at" />


                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="view" runat="server" OnClick="viewProject" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-info"><i class="fa fa-list"></i> View</asp:LinkButton>--%>
                            <asp:LinkButton ID="editTask" runat="server" OnClick="editTask" CommandArgument='<%# Bind("task_id") %>' CssClass="btn btn-success"><i class="fa fa-edit"></i></asp:LinkButton>
                            <asp:LinkButton ID="deleteTask" runat="server" OnClick="deleteTask" OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CommandName="" CommandArgument='<%# Bind("task_id") %>' CssClass="btn btn-danger"><i class="fa fa-trash-o"></i></asp:LinkButton>
                        </ItemTemplate>
                      </asp:TemplateField>  
                
                </Columns>

            
            </asp:GridView>

<%--                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    
                    SelectCommand="select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id order by due_date">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="user_id" SessionField="user" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>


                <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="task_id" DataSourceID="SqlDataSource1"
                    CssClass="table table-striped table-bordered" 
                    onrowdatabound="TaskGridView_RowDataBound" 
                    onrowcreated="GridView1_RowCreated">
                <Columns>
                    <asp:BoundField DataField="task_id" HeaderText="task_id" InsertVisible="False" 
                        ReadOnly="True" SortExpression="task_id" Visible="False" />
                    <asp:BoundField DataField="task_status" HeaderText="Status" 
                        SortExpression="task_status" />
                    <asp:TemplateField HeaderText="Title" SortExpression="task_title">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("task_title") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("task_title") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="project_id" HeaderText="Project" 
                        SortExpression="project_id" />
                    <asp:BoundField DataField="task_priority" HeaderText="Priority" 
                        SortExpression="task_priority" />
                    <asp:BoundField DataField="due_date" HeaderText="Due Date" 
                        SortExpression="due_date" />
                    <asp:BoundField DataField="created_at" HeaderText="Created" 
                        SortExpression="created_at" />
                </Columns>
            
            </asp:GridView>


                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    SelectCommand="SELECT [task_id], [task_title], [project_id], [task_status], [task_priority], [due_date], [created_at] FROM [tasks] WHERE ([creator_id] = @creator_id)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="creator_id" SessionField="user" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>


              <%--<asp:GridView ID="TaskGridView" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="project_id" DataSourceID="SqlDataSource1" CssClass="table table-striped table-bordered" OnRowCreated="GridView1_OnRowCreated" OnRowDataBound="OnRowDataBound">
                  <Columns>
                      <asp:BoundField DataField="project_id" HeaderText="project_id" 
                          InsertVisible="False" ReadOnly="True" SortExpression="Project Id" />
                      <asp:BoundField DataField="project_title" HeaderText="Project Title" 
                          SortExpression="project_title" />
                      <asp:BoundField DataField="status_name" HeaderText="Status" 
                          SortExpression="status_name" />
                      <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="view" runat="server" OnClick="viewProject" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-info"><i class="fa fa-list"></i> View</asp:LinkButton>
                            <asp:LinkButton ID="edit" runat="server" OnClick="editProject" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-success"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                            <asp:LinkButton ID="delete" runat="server" OnClick="deleteProject" OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CommandName="" CommandArgument='<%# Bind("project_id") %>' CssClass="btn btn-danger"><i class="fa fa-trash-o"></i> Delete</asp:LinkButton>
                        </ItemTemplate>
                      </asp:TemplateField>  
                  </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    
                    SelectCommand="select p.project_id, p.project_title, s.status_name from projects p, project_status s where p.project_id in (select distinct(project_id) from project_network where member_id = @user) and s.status_id = p.project_status">
                        <SelectParameters>
                            <asp:SessionParameter
                                Name="user"
                                SessionField="user"
                                DefaultValue="0" />
                        </SelectParameters>
                </asp:SqlDataSource>--%>
                
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
        <!-- /.col -->
      </div>

<%--      <div class="row">
        <div class="col-md-12">
          <div class="box">
            <div class="box-header with-border">
              <h3 class="box-title">Monthly Recap Report</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <div class="btn-group">
                  <button type="button" class="btn btn-box-tool dropdown-toggle" data-toggle="dropdown">
                    <i class="fa fa-wrench"></i></button>
                  <ul class="dropdown-menu" role="menu">
                    <li><a href="#">Action</a></li>
                    <li><a href="#">Another action</a></li>
                    <li><a href="#">Something else here</a></li>
                    <li class="divider"></li>
                    <li><a href="#">Separated link</a></li>
                  </ul>
                </div>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <div class="row">
                <div class="col-md-8">
                  <p class="text-center">
                    <strong>Sales: 1 Jan, 2014 - 30 Jul, 2014</strong>
                  </p>

                  <div class="chart">
                    <!-- Sales Chart Canvas -->
                    <canvas id="salesChart" style="height: 180px;"></canvas>
                  </div>
                  <!-- /.chart-responsive -->
                </div>
                <!-- /.col -->
                <div class="col-md-4">
                  <p class="text-center">
                    <strong>Goal Completion</strong>
                  </p>

                  <div class="progress-group">
                    <span class="progress-text">Add Products to Cart</span>
                    <span class="progress-number"><b>160</b>/200</span>

                    <div class="progress sm">
                      <div class="progress-bar progress-bar-aqua" style="width: 80%"></div>
                    </div>
                  </div>
                  <!-- /.progress-group -->
                  <div class="progress-group">
                    <span class="progress-text">Complete Purchase</span>
                    <span class="progress-number"><b>310</b>/400</span>

                    <div class="progress sm">
                      <div class="progress-bar progress-bar-red" style="width: 80%"></div>
                    </div>
                  </div>
                  <!-- /.progress-group -->
                  <div class="progress-group">
                    <span class="progress-text">Visit Premium Page</span>
                    <span class="progress-number"><b>480</b>/800</span>

                    <div class="progress sm">
                      <div class="progress-bar progress-bar-green" style="width: 80%"></div>
                    </div>
                  </div>
                  <!-- /.progress-group -->
                  <div class="progress-group">
                    <span class="progress-text">Send Inquiries</span>
                    <span class="progress-number"><b>250</b>/500</span>

                    <div class="progress sm">
                      <div class="progress-bar progress-bar-yellow" style="width: 80%"></div>
                    </div>
                  </div>
                  <!-- /.progress-group -->
                </div>
                <!-- /.col -->
              </div>
              <!-- /.row -->
            </div>
            <!-- ./box-body -->
            <div class="box-footer">
              <div class="row">
                <div class="col-sm-3 col-xs-6">
                  <div class="description-block border-right">
                    <span class="description-percentage text-green"><i class="fa fa-caret-up"></i> 17%</span>
                    <h5 class="description-header">$35,210.43</h5>
                    <span class="description-text">TOTAL REVENUE</span>
                  </div>
                  <!-- /.description-block -->
                </div>
                <!-- /.col -->
                <div class="col-sm-3 col-xs-6">
                  <div class="description-block border-right">
                    <span class="description-percentage text-yellow"><i class="fa fa-caret-left"></i> 0%</span>
                    <h5 class="description-header">$10,390.90</h5>
                    <span class="description-text">TOTAL COST</span>
                  </div>
                  <!-- /.description-block -->
                </div>
                <!-- /.col -->
                <div class="col-sm-3 col-xs-6">
                  <div class="description-block border-right">
                    <span class="description-percentage text-green"><i class="fa fa-caret-up"></i> 20%</span>
                    <h5 class="description-header">$24,813.53</h5>
                    <span class="description-text">TOTAL PROFIT</span>
                  </div>
                  <!-- /.description-block -->
                </div>
                <!-- /.col -->
                <div class="col-sm-3 col-xs-6">
                  <div class="description-block">
                    <span class="description-percentage text-red"><i class="fa fa-caret-down"></i> 18%</span>
                    <h5 class="description-header">1200</h5>
                    <span class="description-text">GOAL COMPLETIONS</span>
                  </div>
                  <!-- /.description-block -->
                </div>
              </div>
              <!-- /.row -->
            </div>
            <!-- /.box-footer -->
          </div>
          <!-- /.box -->
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->--%>

      <!-- Main row -->
<%--      <div class="row">
        <!-- Left col -->
        <div class="col-md-8">
          <!-- MAP & BOX PANE -->
          <div class="box box-success">
            <div class="box-header with-border">
              <h3 class="box-title">Visitors Report</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body no-padding">
              <div class="row">
                <div class="col-md-9 col-sm-8">
                  <div class="pad">
                    <!-- Map will be created here -->
                    <div id="world-map-markers" style="height: 325px;"></div>
                  </div>
                </div>
                <!-- /.col -->
                <div class="col-md-3 col-sm-4">
                  <div class="pad box-pane-right bg-green" style="min-height: 280px">
                    <div class="description-block margin-bottom">
                      <div class="sparkbar pad" data-color="#fff">90,70,90,70,75,80,70</div>
                      <h5 class="description-header">8390</h5>
                      <span class="description-text">Visits</span>
                    </div>
                    <!-- /.description-block -->
                    <div class="description-block margin-bottom">
                      <div class="sparkbar pad" data-color="#fff">90,50,90,70,61,83,63</div>
                      <h5 class="description-header">30%</h5>
                      <span class="description-text">Referrals</span>
                    </div>
                    <!-- /.description-block -->
                    <div class="description-block">
                      <div class="sparkbar pad" data-color="#fff">90,50,90,70,61,83,63</div>
                      <h5 class="description-header">70%</h5>
                      <span class="description-text">Organic</span>
                    </div>
                    <!-- /.description-block -->
                  </div>
                </div>
                <!-- /.col -->
              </div>
              <!-- /.row -->
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
          <div class="row">
            <div class="col-md-6">
              <!-- DIRECT CHAT -->
              <div class="box box-warning direct-chat direct-chat-warning">
                <div class="box-header with-border">
                  <h3 class="box-title">Direct Chat</h3>

                  <div class="box-tools pull-right">
                    <span data-toggle="tooltip" title="3 New Messages" class="badge bg-yellow">3</span>
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                      <i class="fa fa-comments"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i>
                    </button>
                  </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                  <!-- Conversations are loaded here -->
                  <div class="direct-chat-messages">
                    <!-- Message. Default to the left -->
                    <div class="direct-chat-msg">
                      <div class="direct-chat-info clearfix">
                        <span class="direct-chat-name pull-left">Alexander Pierce</span>
                        <span class="direct-chat-timestamp pull-right">23 Jan 2:00 pm</span>
                      </div>
                      <!-- /.direct-chat-info -->
                      <img class="direct-chat-img" src="dist/img/user1-128x128.jpg" alt="message user image"><!-- /.direct-chat-img -->
                      <div class="direct-chat-text">
                        Is this template really for free? That's unbelievable!
                      </div>
                      <!-- /.direct-chat-text -->
                    </div>
                    <!-- /.direct-chat-msg -->

                    <!-- Message to the right -->
                    <div class="direct-chat-msg right">
                      <div class="direct-chat-info clearfix">
                        <span class="direct-chat-name pull-right">Sarah Bullock</span>
                        <span class="direct-chat-timestamp pull-left">23 Jan 2:05 pm</span>
                      </div>
                      <!-- /.direct-chat-info -->
                      <img class="direct-chat-img" src="dist/img/user3-128x128.jpg" alt="message user image"><!-- /.direct-chat-img -->
                      <div class="direct-chat-text">
                        You better believe it!
                      </div>
                      <!-- /.direct-chat-text -->
                    </div>
                    <!-- /.direct-chat-msg -->

                    <!-- Message. Default to the left -->
                    <div class="direct-chat-msg">
                      <div class="direct-chat-info clearfix">
                        <span class="direct-chat-name pull-left">Alexander Pierce</span>
                        <span class="direct-chat-timestamp pull-right">23 Jan 5:37 pm</span>
                      </div>
                      <!-- /.direct-chat-info -->
                      <img class="direct-chat-img" src="dist/img/user1-128x128.jpg" alt="message user image"><!-- /.direct-chat-img -->
                      <div class="direct-chat-text">
                        Working with AdminLTE on a great new app! Wanna join?
                      </div>
                      <!-- /.direct-chat-text -->
                    </div>
                    <!-- /.direct-chat-msg -->

                    <!-- Message to the right -->
                    <div class="direct-chat-msg right">
                      <div class="direct-chat-info clearfix">
                        <span class="direct-chat-name pull-right">Sarah Bullock</span>
                        <span class="direct-chat-timestamp pull-left">23 Jan 6:10 pm</span>
                      </div>
                      <!-- /.direct-chat-info -->
                      <img class="direct-chat-img" src="dist/img/user3-128x128.jpg" alt="message user image"><!-- /.direct-chat-img -->
                      <div class="direct-chat-text">
                        I would love to.
                      </div>
                      <!-- /.direct-chat-text -->
                    </div>
                    <!-- /.direct-chat-msg -->

                  </div>
                  <!--/.direct-chat-messages-->

                  <!-- Contacts are loaded here -->
                  <div class="direct-chat-contacts">
                    <ul class="contacts-list">
                      <li>
                        <a href="#">
                          <img class="contacts-list-img" src="dist/img/user1-128x128.jpg" alt="User Image">

                          <div class="contacts-list-info">
                                <span class="contacts-list-name">
                                  Count Dracula
                                  <small class="contacts-list-date pull-right">2/28/2015</small>
                                </span>
                            <span class="contacts-list-msg">How have you been? I was...</span>
                          </div>
                          <!-- /.contacts-list-info -->
                        </a>
                      </li>
                      <!-- End Contact Item -->
                      <li>
                        <a href="#">
                          <img class="contacts-list-img" src="dist/img/user7-128x128.jpg" alt="User Image">

                          <div class="contacts-list-info">
                                <span class="contacts-list-name">
                                  Sarah Doe
                                  <small class="contacts-list-date pull-right">2/23/2015</small>
                                </span>
                            <span class="contacts-list-msg">I will be waiting for...</span>
                          </div>
                          <!-- /.contacts-list-info -->
                        </a>
                      </li>
                      <!-- End Contact Item -->
                      <li>
                        <a href="#">
                          <img class="contacts-list-img" src="dist/img/user3-128x128.jpg" alt="User Image">

                          <div class="contacts-list-info">
                                <span class="contacts-list-name">
                                  Nadia Jolie
                                  <small class="contacts-list-date pull-right">2/20/2015</small>
                                </span>
                            <span class="contacts-list-msg">I'll call you back at...</span>
                          </div>
                          <!-- /.contacts-list-info -->
                        </a>
                      </li>
                      <!-- End Contact Item -->
                      <li>
                        <a href="#">
                          <img class="contacts-list-img" src="dist/img/user5-128x128.jpg" alt="User Image">

                          <div class="contacts-list-info">
                                <span class="contacts-list-name">
                                  Nora S. Vans
                                  <small class="contacts-list-date pull-right">2/10/2015</small>
                                </span>
                            <span class="contacts-list-msg">Where is your new...</span>
                          </div>
                          <!-- /.contacts-list-info -->
                        </a>
                      </li>
                      <!-- End Contact Item -->
                      <li>
                        <a href="#">
                          <img class="contacts-list-img" src="dist/img/user6-128x128.jpg" alt="User Image">

                          <div class="contacts-list-info">
                                <span class="contacts-list-name">
                                  John K.
                                  <small class="contacts-list-date pull-right">1/27/2015</small>
                                </span>
                            <span class="contacts-list-msg">Can I take a look at...</span>
                          </div>
                          <!-- /.contacts-list-info -->
                        </a>
                      </li>
                      <!-- End Contact Item -->
                      <li>
                        <a href="#">
                          <img class="contacts-list-img" src="dist/img/user8-128x128.jpg" alt="User Image">

                          <div class="contacts-list-info">
                                <span class="contacts-list-name">
                                  Kenneth M.
                                  <small class="contacts-list-date pull-right">1/4/2015</small>
                                </span>
                            <span class="contacts-list-msg">Never mind I found...</span>
                          </div>
                          <!-- /.contacts-list-info -->
                        </a>
                      </li>
                      <!-- End Contact Item -->
                    </ul>
                    <!-- /.contatcts-list -->
                  </div>
                  <!-- /.direct-chat-pane -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer">
                  <form action="#" method="post">
                    <div class="input-group">
                      <input type="text" name="message" placeholder="Type Message ..." class="form-control">
                          <span class="input-group-btn">
                            <button type="button" class="btn btn-warning btn-flat">Send</button>
                          </span>
                    </div>
                  </form>
                </div>
                <!-- /.box-footer-->
              </div>
              <!--/.direct-chat -->
            </div>
            <!-- /.col -->

            <div class="col-md-6">
              <!-- USERS LIST -->
              <div class="box box-danger">
                <div class="box-header with-border">
                  <h3 class="box-title">Latest Members</h3>

                  <div class="box-tools pull-right">
                    <span class="label label-danger">8 New Members</span>
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i>
                    </button>
                  </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body no-padding">
                  <ul class="users-list clearfix">
                    <li>
                      <img src="dist/img/user1-128x128.jpg" alt="User Image">
                      <a class="users-list-name" href="#">Alexander Pierce</a>
                      <span class="users-list-date">Today</span>
                    </li>
                    <li>
                      <img src="dist/img/user8-128x128.jpg" alt="User Image">
                      <a class="users-list-name" href="#">Norman</a>
                      <span class="users-list-date">Yesterday</span>
                    </li>
                    <li>
                      <img src="dist/img/user7-128x128.jpg" alt="User Image">
                      <a class="users-list-name" href="#">Jane</a>
                      <span class="users-list-date">12 Jan</span>
                    </li>
                    <li>
                      <img src="dist/img/user6-128x128.jpg" alt="User Image">
                      <a class="users-list-name" href="#">John</a>
                      <span class="users-list-date">12 Jan</span>
                    </li>
                    <li>
                      <img src="dist/img/user2-160x160.jpg" alt="User Image">
                      <a class="users-list-name" href="#">Alexander</a>
                      <span class="users-list-date">13 Jan</span>
                    </li>
                    <li>
                      <img src="dist/img/user5-128x128.jpg" alt="User Image">
                      <a class="users-list-name" href="#">Sarah</a>
                      <span class="users-list-date">14 Jan</span>
                    </li>
                    <li>
                      <img src="dist/img/user4-128x128.jpg" alt="User Image">
                      <a class="users-list-name" href="#">Nora</a>
                      <span class="users-list-date">15 Jan</span>
                    </li>
                    <li>
                      <img src="dist/img/user3-128x128.jpg" alt="User Image">
                      <a class="users-list-name" href="#">Nadia</a>
                      <span class="users-list-date">15 Jan</span>
                    </li>
                  </ul>
                  <!-- /.users-list -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer text-center">
                  <a href="javascript:void(0)" class="uppercase">View All Users</a>
                </div>
                <!-- /.box-footer -->
              </div>
              <!--/.box -->
            </div>
            <!-- /.col -->
          </div>
          <!-- /.row -->

          <!-- TABLE: LATEST ORDERS -->
          <div class="box box-info">
            <div class="box-header with-border">
              <h3 class="box-title">Latest Orders</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <div class="table-responsive">
                <table class="table no-margin">
                  <thead>
                  <tr>
                    <th>Order ID</th>
                    <th>Item</th>
                    <th>Status</th>
                    <th>Popularity</th>
                  </tr>
                  </thead>
                  <tbody>
                  <tr>
                    <td><a href="pages/examples/invoice.html">OR9842</a></td>
                    <td>Call of Duty IV</td>
                    <td><span class="label label-success">Shipped</span></td>
                    <td>
                      <div class="sparkbar" data-color="#00a65a" data-height="20">90,80,90,-70,61,-83,63</div>
                    </td>
                  </tr>
                  <tr>
                    <td><a href="pages/examples/invoice.html">OR1848</a></td>
                    <td>Samsung Smart TV</td>
                    <td><span class="label label-warning">Pending</span></td>
                    <td>
                      <div class="sparkbar" data-color="#f39c12" data-height="20">90,80,-90,70,61,-83,68</div>
                    </td>
                  </tr>
                  <tr>
                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                    <td>iPhone 6 Plus</td>
                    <td><span class="label label-danger">Delivered</span></td>
                    <td>
                      <div class="sparkbar" data-color="#f56954" data-height="20">90,-80,90,70,-61,83,63</div>
                    </td>
                  </tr>
                  <tr>
                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                    <td>Samsung Smart TV</td>
                    <td><span class="label label-info">Processing</span></td>
                    <td>
                      <div class="sparkbar" data-color="#00c0ef" data-height="20">90,80,-90,70,-61,83,63</div>
                    </td>
                  </tr>
                  <tr>
                    <td><a href="pages/examples/invoice.html">OR1848</a></td>
                    <td>Samsung Smart TV</td>
                    <td><span class="label label-warning">Pending</span></td>
                    <td>
                      <div class="sparkbar" data-color="#f39c12" data-height="20">90,80,-90,70,61,-83,68</div>
                    </td>
                  </tr>
                  <tr>
                    <td><a href="pages/examples/invoice.html">OR7429</a></td>
                    <td>iPhone 6 Plus</td>
                    <td><span class="label label-danger">Delivered</span></td>
                    <td>
                      <div class="sparkbar" data-color="#f56954" data-height="20">90,-80,90,70,-61,83,63</div>
                    </td>
                  </tr>
                  <tr>
                    <td><a href="pages/examples/invoice.html">OR9842</a></td>
                    <td>Call of Duty IV</td>
                    <td><span class="label label-success">Shipped</span></td>
                    <td>
                      <div class="sparkbar" data-color="#00a65a" data-height="20">90,80,90,-70,61,-83,63</div>
                    </td>
                  </tr>
                  </tbody>
                </table>
              </div>
              <!-- /.table-responsive -->
            </div>
            <!-- /.box-body -->
            <div class="box-footer clearfix">
              <a href="javascript:void(0)" class="btn btn-sm btn-info btn-flat pull-left">Place New Order</a>
              <a href="javascript:void(0)" class="btn btn-sm btn-default btn-flat pull-right">View All Orders</a>
            </div>
            <!-- /.box-footer -->
          </div>
          <!-- /.box -->
        </div>
        <!-- /.col -->

        <div class="col-md-4">
          <!-- Info Boxes Style 2 -->
          <div class="info-box bg-yellow">
            <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Inventory</span>
              <span class="info-box-number">5,200</span>

              <div class="progress">
                <div class="progress-bar" style="width: 50%"></div>
              </div>
                  <span class="progress-description">
                    50% Increase in 30 Days
                  </span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
          <div class="info-box bg-green">
            <span class="info-box-icon"><i class="ion ion-ios-heart-outline"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Mentions</span>
              <span class="info-box-number">92,050</span>

              <div class="progress">
                <div class="progress-bar" style="width: 20%"></div>
              </div>
                  <span class="progress-description">
                    20% Increase in 30 Days
                  </span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
          <div class="info-box bg-red">
            <span class="info-box-icon"><i class="ion ion-ios-cloud-download-outline"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Downloads</span>
              <span class="info-box-number">114,381</span>

              <div class="progress">
                <div class="progress-bar" style="width: 70%"></div>
              </div>
                  <span class="progress-description">
                    70% Increase in 30 Days
                  </span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
          <div class="info-box bg-aqua">
            <span class="info-box-icon"><i class="ion-ios-chatbubble-outline"></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Direct Messages</span>
              <span class="info-box-number">163,921</span>

              <div class="progress">
                <div class="progress-bar" style="width: 40%"></div>
              </div>
                  <span class="progress-description">
                    40% Increase in 30 Days
                  </span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->

          <div class="box box-default">
            <div class="box-header with-border">
              <h3 class="box-title">Browser Usage</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <div class="row">
                <div class="col-md-8">
                  <div class="chart-responsive">
                    <canvas id="pieChart" height="150"></canvas>
                  </div>
                  <!-- ./chart-responsive -->
                </div>
                <!-- /.col -->
                <div class="col-md-4">
                  <ul class="chart-legend clearfix">
                    <li><i class="fa fa-circle-o text-red"></i> Chrome</li>
                    <li><i class="fa fa-circle-o text-green"></i> IE</li>
                    <li><i class="fa fa-circle-o text-yellow"></i> FireFox</li>
                    <li><i class="fa fa-circle-o text-aqua"></i> Safari</li>
                    <li><i class="fa fa-circle-o text-light-blue"></i> Opera</li>
                    <li><i class="fa fa-circle-o text-gray"></i> Navigator</li>
                  </ul>
                </div>
                <!-- /.col -->
              </div>
              <!-- /.row -->
            </div>
            <!-- /.box-body -->
            <div class="box-footer no-padding">
              <ul class="nav nav-pills nav-stacked">
                <li><a href="#">United States of America
                  <span class="pull-right text-red"><i class="fa fa-angle-down"></i> 12%</span></a></li>
                <li><a href="#">India <span class="pull-right text-green"><i class="fa fa-angle-up"></i> 4%</span></a>
                </li>
                <li><a href="#">China
                  <span class="pull-right text-yellow"><i class="fa fa-angle-left"></i> 0%</span></a></li>
              </ul>
            </div>
            <!-- /.footer -->
          </div>
          <!-- /.box -->

          <!-- PRODUCT LIST -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Recently Added Products</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <ul class="products-list product-list-in-box">
                <li class="item">
                  <div class="product-img">
                    <img src="dist/img/default-50x50.gif" alt="Product Image">
                  </div>
                  <div class="product-info">
                    <a href="javascript:void(0)" class="product-title">Samsung TV
                      <span class="label label-warning pull-right">$1800</span></a>
                        <span class="product-description">
                          Samsung 32" 1080p 60Hz LED Smart HDTV.
                        </span>
                  </div>
                </li>
                <!-- /.item -->
                <li class="item">
                  <div class="product-img">
                    <img src="dist/img/default-50x50.gif" alt="Product Image">
                  </div>
                  <div class="product-info">
                    <a href="javascript:void(0)" class="product-title">Bicycle
                      <span class="label label-info pull-right">$700</span></a>
                        <span class="product-description">
                          26" Mongoose Dolomite Men's 7-speed, Navy Blue.
                        </span>
                  </div>
                </li>
                <!-- /.item -->
                <li class="item">
                  <div class="product-img">
                    <img src="dist/img/default-50x50.gif" alt="Product Image">
                  </div>
                  <div class="product-info">
                    <a href="javascript:void(0)" class="product-title">Xbox One <span class="label label-danger pull-right">$350</span></a>
                        <span class="product-description">
                          Xbox One Console Bundle with Halo Master Chief Collection.
                        </span>
                  </div>
                </li>
                <!-- /.item -->
                <li class="item">
                  <div class="product-img">
                    <img src="dist/img/default-50x50.gif" alt="Product Image">
                  </div>
                  <div class="product-info">
                    <a href="javascript:void(0)" class="product-title">PlayStation 4
                      <span class="label label-success pull-right">$399</span></a>
                        <span class="product-description">
                          PlayStation 4 500GB Console (PS4)
                        </span>
                  </div>
                </li>
                <!-- /.item -->
              </ul>
            </div>
            <!-- /.box-body -->
            <div class="box-footer text-center">
              <a href="javascript:void(0)" class="uppercase">View All Products</a>
            </div>
            <!-- /.box-footer -->
          </div>
          <!-- /.box -->
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->--%>
    </section>
    <!-- /.content -->

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<!-- ChartJS 1.0.1 -->
<script src="plugins/chartjs/Chart.min.js"></script>
<!-- AdminLTE dashboard demo (This is only for demo purposes) -->
<!--script src="dist/js/pages/dashboard2.js"></script-->

<!-- DataTables -->
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>
<!-- page script -->
    <script>
        $(function () {
            //$("#GridView1").DataTable();
            // Using aoColumnDefs
            $('#ContentPlaceHolder1_TaskGridView').dataTable();
        });
    </script>
</asp:Content>

