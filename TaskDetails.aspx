<%@ Page Title="Task Details" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaskDetails.aspx.cs" Inherits="TaskDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
    .users-list>li 
    {
        width:12%;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        <div runat="server" id="task_title">
        Task Title
        </div>
        <small><!--advanced tables--></small>
      </h1>
      
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="Tasks.aspx">Tasks</a></li>
        <li class="active">Task Details</li>
      </ol>
    </section>

    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          

          
          <%--<asp:TextBox ID="descriptionTextBox" runat="server" CssClass="box-body" 
                ></asp:TextBox>--%>
          <!-- /.box -->

          <!-- /.box -->
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Task Details</h3>
            <asp:LinkButton ID="EditTaskButton" runat="server" CssClass="btn btn-success" 
                    onclick="EditTaskButton_Click"><i class="fa fa-edit"></i> Edit Task</asp:LinkButton>
              <%--<a href="EditTask.aspx" class="btn btn-success" style="margin-left:30px; float:right"><i class="fa fa-edit"></i> Edit Task</a>--%>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
        <asp:DetailsView ID="TaskDetailsView" runat="server" 
                    CssClass="table table-striped table-bordered" AutoGenerateRows="False" 
                    DataSourceID="SqlDataSource1" ondatabound="TaskDetailsView_DataBound">
            <Fields>
                <asp:BoundField DataField="project_id" HeaderText="Project" 
                    SortExpression="project_id" />
                <asp:BoundField DataField="creator_id" HeaderText="Admin" 
                    SortExpression="creator_id" />
                <asp:BoundField DataField="task_status" HeaderText="Status" 
                    SortExpression="task_status" />
                <asp:BoundField DataField="task_priority" HeaderText="Priority" 
                    SortExpression="task_priority" />
                <asp:BoundField DataField="due_date" HeaderText="Due Date" 
                    SortExpression="due_date" />
                <asp:BoundField DataField="created_at" HeaderText="Created On" 
                    SortExpression="created_at" />
            </Fields>
                </asp:DetailsView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    SelectCommand="SELECT [project_id], [creator_id], [task_status], [task_priority], [due_date], [created_at] FROM [tasks] WHERE ([task_id] = @task_id)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="task_id" SessionField="task_id" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <%--<asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSource1" CssClass="table table-striped table-bordered">
                    <Fields>
                        <asp:BoundField DataField="Admin" HeaderText="Admin" 
                            SortExpression="Admin" ReadOnly="True" />
                        <asp:BoundField DataField="created_at" HeaderText="Created On" 
                            SortExpression="created_at" />
                        <asp:BoundField DataField="updated_at" HeaderText="Last Updated" 
                            SortExpression="updated_at" />
                    </Fields>
                </asp:DetailsView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    SelectCommand="select u.first_name+' '+u.last_name as Admin, p.created_at, p.updated_at from users u, projects p where p.project_id = @project and u.user_id = p.created_by">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="project" SessionField="pid" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

                    <!-- /.box -->
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Description</h3>
            </div>
            <!-- /.box-header -->
            <div runat="server" class="box-body" id="descBody"></div>

            <%--<asp:Label ID="lblName" runat="server" Text="Label" CssClass="editable box-body"></asp:Label>--%>

            <!-- /.box-body -->
          </div>

          <!-- /.box -->
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Members</h3>
            </div>
            <!-- /.box-header -->
            <!--div class="box-body" id="memberBox"></div-->
            <div class="box-body no-padding">
                  <ul runat="server" class="users-list clearfix" id="users_list"></ul>
                  <!-- /.users-list -->
                </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

          <!-- Task Timeline box -->
          <div class="box">
            <div class="box-header">
              <i class="fa fa-comments-o"></i>

              <h3 class="box-title">Timeline</h3>
              
              <div class="box-tools pull-right" data-toggle="tooltip" title="Select Project" style="width:215px;">
                <!--div class="form-group">
                    <div class="col-sm-12"-->
                        <%--<asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" AutoPostBack="true" onselectedindexchanged="loadMessages"></asp:DropDownList>--%>          
                    <!--/div>
                </div-->
                <div class="btn-group" data-toggle="btn-toggle">
                  <!--button type="button" class="btn btn-default btn-sm active"><i class="fa fa-square text-green"></i>
                  </button>
                  <button type="button" class="btn btn-default btn-sm"><i class="fa fa-square text-red"></i></button-->
                </div>
              </div>
            </div>

            <div runat="server" class="box-body chat" id="timelinebox">
              <!-- chat item -->
              <!-- /.item -->
            </div>
            <!-- /.chat -->
            <div class="box-footer">
              <div class="input-group">
                <!--input class="form-control" placeholder="Type message..." id="msgbody"-->
                <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Type message..."></asp:TextBox>--%>
                <div class="input-group-btn">
                  <!--button type="button" class="btn btn-success" style="height:34px;"><i class="fa fa-plus"></i></button-->
                  <%--<asp:LinkButton OnClick="sendMsg" ID="LinkButton1" runat="server" CssClass="btn btn-success"><i class="fa fa-plus" style="position:relative; top:4px"></i></asp:LinkButton>--%>
                  <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                  </asp:ScriptManager>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <ContentTemplate>
                       <asp:Button ID="Button1" runat="server" Text="Send" CssClass="btn btn-success" OnClick="sendMsg"></asp:Button>
                     </ContentTemplate>
                  </asp:UpdatePanel>--%>
                  <%--<asp:Button ID="Button1" runat="server" Text="Send" CssClass="btn btn-success" OnClick="sendMsg"></asp:Button>--%>
                </div>
              </div>
            </div>
          </div>
          <!-- /.box (task timeline box) -->



        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

