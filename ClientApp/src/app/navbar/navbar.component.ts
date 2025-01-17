import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  model:any={};

  constructor(public autService: AuthService, private router: Router,private alertify:AlertifyService) { }

  ngOnInit(): void {
  }

 login(){
  this.autService.login(this.model).subscribe(next=> {
  this.alertify.success("Login Başarılı");
    this.router.navigate(['/members']);
  },error=>{
    this.alertify.error(error);
  })
 }

 loggedIn(){
  return this.autService.loggedIn();
 }

 logout(){
  localStorage.removeItem("token");
  this.alertify.warning("Logout Başarılı");
  this.router.navigate(['/members']);
 }

}
