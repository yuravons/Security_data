#include "mainwindow.h"
#include "ui_mainwindow.h"


MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    QFile file("C://Users//hp//Documents//SPD//input.txt");
    if (!file.open(QIODevice::ReadOnly | QIODevice::Text))
        return ;
    QString line;
    QTextStream in(&file);
    while (!in.atEnd())
        line = in.readLine();
    ui->lineEdit->setText(line);
    file.close();
    obj = new Generator();
}

MainWindow::~MainWindow()
{
    delete ui;
    delete obj;
}

void MainWindow::on_pushButton_clicked()
{
    uint size = ui->lineEdit->text().toInt();
    if(size > 0) {
        obj->setSize(size);
        obj->generateValues();
        printValues(obj->getArray());
    }
}

void MainWindow::printValues(uint *array)
{
    for(uint n = 0; n < obj->getSize(); ++n ) {
        if(n == (obj->getSize() - 1))
            ui->textEdit->insertPlainText(QString::number(array[n]));
        else
            ui->textEdit->insertPlainText(QString::number(array[n]) + ", ");
    }
}

void MainWindow::on_pushButton_2_clicked()
{
    ui->textEdit->clear();
    ui->lineEdit->clear();
}

void MainWindow::on_pushButton_3_clicked()
{
    QFile file("C://Users//hp//Documents//SPD//output.txt");
    if (!file.open(QIODevice::WriteOnly | QIODevice::Append))
        return;
    QTextStream out(&file);

    for(uint n = 0; n < obj->getSize(); ++n ) {
        if(n == (obj->getSize() - 1))
            out << QString::number(obj->getArray()[n]);
        else
            out << QString::number(obj->getArray()[n]) << ", ";
    }
    file.close();
}
