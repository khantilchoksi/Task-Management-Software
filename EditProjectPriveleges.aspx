<%@ Page Title="Edit Project Priveleges" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditProjectPriveleges.aspx.cs" Inherits="EditProjectPriveleges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        h4 
        {
            font-size: 15px;
            margin-bottom: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!-- Content Header (Page header) -->
<div runat="server" class="alert alert-success alert-dismissible" id="SuccessBox" style="display:none; margin-bottom:0px;">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <h4><i class="icon fa fa-check"></i> Changes Saved!</h4>
              </div>
    <section class="content-header">
      <h1>
        <div runat="server" id="ptitle">
        Edit Priveleges : 
        </div>
        <small><!--advanced tables--></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="Projects.aspx">Projects</a></li>
        <li><a href="ProjectDetails.aspx">Project Details</a></li>
        <li class="active">Edit Priveleges</li>
      </ol>
    </section>
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          
          <!-- /.box -->
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Description</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body" runat="server" id="boxBody">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource1" DataKeyNames="member_id">
                    <Columns>
                        <asp:BoundField DataField="member_id" HeaderText="Member ID" ReadOnly="True" 
                            SortExpression="member_id" Visible="false" />
                        <asp:BoundField DataField="Member" HeaderText="Member" ReadOnly="True" 
                            SortExpression="Member" />
                        <%--<asp:BoundField DataField="read_right" HeaderText="read_right" 
                            SortExpression="read_right" />
                        <asp:BoundField DataField="write_right" HeaderText="write_right" 
                            SortExpression="write_right" />--%>
                        <asp:TemplateField HeaderText="Can Read">
                            <ItemTemplate>
                                <asp:CheckBox ID="read" runat="server" Checked='<%# Convert.ToBoolean(Eval("read_right")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Can Write">
                            <ItemTemplate>
                                <asp:CheckBox ID="write" runat="server" Checked='<%# Convert.ToBoolean(Eval("write_right")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:testcon %>" 
                    SelectCommand="select first_name+' '+last_name as Member, member_id, read_right, write_right from users u, project_network pn where pn.project_id = @proj and user_id = member_id and user_id != @user">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="proj" SessionField="pid" />
                        <asp:SessionParameter DefaultValue="0" Name="user" SessionField="user" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <a href="ProjectDetails.aspx" class="btn btn-default pull-left">Back</a>
                <asp:Button ID="Button1" CssClass="btn btn-primary pull-right" runat="server" Text="Save Changes" onclick="Button1_Click"></asp:Button>
            </div>
            <!-- /.box-body -->
          </div>
        </div>
       </div>
     </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

