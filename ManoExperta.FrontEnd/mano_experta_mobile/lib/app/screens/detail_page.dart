import 'package:flutter/material.dart';
import 'package:mano_experta_mobile/app/data/models/professional.dart';

class DetailPage extends StatelessWidget {
  final Professional professional;

  DetailPage(this.professional);
  @override
  Widget build(BuildContext context) {    
     return new Scaffold(
              appBar: new AppBar(
              title: new Text(professional.userName),
              )
         );
  }
}