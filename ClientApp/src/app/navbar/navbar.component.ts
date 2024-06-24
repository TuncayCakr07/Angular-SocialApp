import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  model:any={};

  constructor(private autService:AuthService,private router:Router) { }

  ngOnInit(): void {
  }

 login(){
  this.autService.login(this.model).subscribe(next=> {
    console.log("Login Başarılı");
    this.router.navigate(['/members']);
  },error=>{
    console.log("Login Hatalı");
  })
 }

 loggedIn(){
  const token=localStorage.getItem("token");
  return token ? true: false;
 }

 logout(){
  localStorage.removeItem("token");
  console.log("Logout Başarılı");
  this.router.navigate(['/members']);
 }

}
