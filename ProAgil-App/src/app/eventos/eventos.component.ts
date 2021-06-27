import { HttpClient } from '@angular/common/http';
import { Component, OnInit, resolveForwardRef } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
 
  eventos: any = [];
  imagemLargura: number = 50;
  imagemMargem: number = 2;
  mostrarImg = false;
  filtroLista = "";
  constructor(private http: HttpClient) { }

  ngOnInit() 
  {
    this.getEventos();
  }
  alternarImagem(){
    this.mostrarImg = !this.mostrarImg;
  }
  getEventos()
  {
    this.http.get('http://localhost:5000/values').subscribe(response => {
      this.eventos = response;
      //console.log("OK");
    }, error => {
      console.log(error);
    });
  }
}
