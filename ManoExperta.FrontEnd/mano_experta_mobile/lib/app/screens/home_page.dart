import 'dart:ui';

import 'package:flutter/material.dart';
import '../data/models/professional.dart';
import '../data/service/professional_service.dart';

class HomePage extends StatefulWidget {
  const HomePage({Key? key}) : super(key: key);
  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  String selectedCategory =
      "PLMR"; // Nuevo estado para almacenar la categoría seleccionada
  List<Professional>? professionals;
  bool isLoaded = false;
  @override
  void initState() {
    super.initState();
    loadProfessionals(selectedCategory);
  }

  Future<void> loadProfessionals(String? value) async {
    final professionalService = ProfessionalService();
    professionals = await professionalService.getProfessionalsByCaregory(value);
    setState(() {
      isLoaded = true;
    });
  }
   final topAppBar = AppBar(
      elevation: 0.1,
      backgroundColor:const Color.fromARGB(255, 233, 237, 245),
      title:  const Text("Mano Experta",
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                      textAlign: TextAlign.center),
    );

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Color.fromARGB(255, 233, 237, 245),
       appBar: topAppBar,
      body: isLoaded && professionals != null
          ? SafeArea(
              child: Column(
                children: [
                  // const Text("Mano Experta",
                  //     style:
                  //         TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                  //     textAlign: TextAlign.center),
                  
                  const SizedBox(height: 24),
                  Row(
                    children: [
                      const SizedBox(width: 8),
                      const Text("Categoria:"),
                      const SizedBox(width: 8),
                      DropdownButton<String>(
                          value: selectedCategory,
                          onChanged: (String? newValue) {
                            setState(() {
                              selectedCategory = newValue!;
                              loadProfessionals(
                                  selectedCategory); // Volver a cargar profesionales cuando se cambie la categoría
                            });
                          },
                          items: const [
                            DropdownMenuItem(
                                child: Text("Plomero"), value: "PLMR"),
                            DropdownMenuItem(
                                child: Text("Electricista"), value: "ELTC"),
                          ]),
                    ],
                  ),
                  const SizedBox(height: 16),
                  const Row(
                    children: [
                      Expanded(
                          child: Text('Nombre',
                              textAlign: TextAlign.center,
                              style: TextStyle(fontWeight: FontWeight.bold))),
                      Expanded(
                          child: Text('Telefono',
                              textAlign: TextAlign.center,
                              style: TextStyle(fontWeight: FontWeight.bold))),
                      Expanded(
                          child: Text('Email',
                              textAlign: TextAlign.center,
                              style: TextStyle(fontWeight: FontWeight.bold))),
                    ],
                  ),
                  const SizedBox(height: 8),
                  Expanded(
                    child: SingleChildScrollView(
                      child: Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: Column(
                          children: professionals!
                              .map(
                                (professional) => Column(
                                  children: [
                                    const SizedBox(height: 4),
                                    Row(
                                      children: [
                                        Expanded(
                                            child: Text(
                                                professional.fullName ??
                                                    'No name')),
                                        Expanded(
                                          child: Text(professional.phoneNumbers
                                                  .firstOrNull!.number
                                                  .toString() ??
                                              'No phone number'),
                                        ),
                                        Expanded(
                                            child: Text(professional
                                                    .emails.firstOrNull!.address
                                                    .toString() ??
                                                'No email')),
                                      ],
                                    ),
                                    const SizedBox(height: 4),
                                    const Divider(),
                                  ],
                                ),
                              )
                              .toList(),
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            )
          : const Center(child: CircularProgressIndicator()),
    );
  }
}

const List<String> list = <String>['One', 'Two', 'Three', 'Four'];

class DropdownMenuExample extends StatefulWidget {
  const DropdownMenuExample({super.key});

  @override
  State<DropdownMenuExample> createState() => _DropdownMenuExampleState();
}

class _DropdownMenuExampleState extends State<DropdownMenuExample> {
  String dropdownValue = list.first;

  @override
  Widget build(BuildContext context) {
    return DropdownMenu<String>(
      initialSelection: list.first,
      onSelected: (String? value) {
        // This is called when the user selects an item.
        setState(() {
          dropdownValue = value!;
        });
      },
      dropdownMenuEntries: list.map<DropdownMenuEntry<String>>((String value) {
        return DropdownMenuEntry<String>(value: value, label: value);
      }).toList(),
    );
  }
}
