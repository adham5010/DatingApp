import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_Services/Auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  Model:any = {};
  IsAuthinticated = false;
  constructor(private Auth:AuthService) { }

  ngOnInit() {
    this.IsAuthinticated = this.Auth.IsAuth();
  }

  Login(){
    this.Auth.login(this.Model).subscribe(next =>{alert('s')},err=>{alert("err")};
    this.IsAuthinticated = this.Auth.IsAuth();
  }

  logOut(){
    this.Auth.Logout();
    this.IsAuthinticated = this.Auth.IsAuth();
  }
}
