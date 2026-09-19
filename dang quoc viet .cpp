#include <iostream>
#include <string>
#include <vector>
#include <iomanip>

using namespace std;

struct HangHoa {
    string maHang;
    string tenHang;
    string ngayXuatHang;
    float giaXuatHang;   
};

void nhapHangHoa(HangHoa &hh) {
    cout << "  Nhap ma hang: ";
    getline(cin, hh.maHang);
    cout << "  Nhap ten hang: ";
    getline(cin, hh.tenHang);
    cout << "  Nhap ngay xuat hang (dd/mm/yyyy): ";
    getline(cin, hh.ngayXuatHang);
    cout << "  Nhap gia xuat hang (trieu dong): ";
    cin >> hh.giaXuatHang;
    cin.ignore(); 
}

void xuatHangHoa(const HangHoa &hh) {
    cout << left << setw(15) << hh.maHang 
         << setw(25) << hh.tenHang 
         << setw(20) << hh.ngayXuatHang 
         << fixed << setprecision(2) << hh.giaXuatHang << endl;
}

void nhapDanhSach(vector<HangHoa> &ds, int n) {
    ds.resize(n);
    for (int i = 0; i < n; i++) {
        cout << "\n--- Nhap thông tin hang hoa thu " << i + 1 << " ---" << endl;
        nhapHangHoa(ds[i]);
    }
}

void xuatDanhSach(const vector<HangHoa> &ds) {
    if (ds.empty()) {
        cout << "Danh sach trong!" << endl;
        return;
    }
    cout << "\n-------------------------------------------------------------------" << endl;
    cout << left << setw(15) << "Ma HH" 
         << setw(25) << "Ten HH" 
         << setw(20) << "Ngay Xuat" 
         << "Gia Xuat (Trieu)" << endl;
    cout << "-------------------------------------------------------------------" << endl;
    for (const auto &hh : ds) {
        xuatHangHoa(hh);
    }
    cout << "-------------------------------------------------------------------" << endl;
}

void selectionSort(vector<HangHoa> &ds) {
    int n = ds.size();
    for (int i = 0; i < n - 1; i++) {
        int minIdx = i;
        for (int j = i + 1; j < n; j++) {
            if (ds[j].giaXuatHang < ds[minIdx].giaXuatHang) {
                minIdx = j;
            }
        }
        if (minIdx != i) {
            swap(ds[i], ds[minIdx]);
        }
    }
}

void timKiemNhiPhan(const vector<HangHoa> &ds, float X) {
    int left = 0, right = ds.size() - 1;
    int foundIdx = -1;

    while (left <= right) {
        int mid = left + (right - left) / 2;
        if (ds[mid].giaXuatHang == X) {
            foundIdx = mid;
            break;
        } else if (ds[mid].giaXuatHang < X) {
            left = mid + 1;
        } else {
            right = mid - 1;
        }
    }

    if (foundIdx == -1) {
        cout << "\nKhong tim thay hang hoa nao co gia xuat bang " << X << " trieu dong." << endl;
        return;
    }

    
    int start = foundIdx;
    while (start > 0 && ds[start - 1].giaXuatHang == X) {
        start--;
    }

    int end = foundIdx;
    while (end < (int)ds.size() - 1 && ds[end + 1].giaXuatHang == X) {
        end++;
    }

    cout << "\n=== KET QUA TIM KIEM HANG HOA CO GIA = " << X << " TRIEU DONG ===" << endl;
    cout << left << setw(15) << "Ma HH" 
         << setw(25) << "Ten HH" 
         << setw(20) << "Ngay Xuat" 
         << "Gia Xuat (Trieu)" << endl;
    cout << "-------------------------------------------------------------------" << endl;
    for (int i = start; i <= end; i++) {
        xuatHangHoa(ds[i]);
    }
    cout << "-------------------------------------------------------------------" << endl;
}


int main() {
    vector<HangHoa> ds;
    int n;

    cout << "Nhap so luong hang hoa (n): ";
    cin >> n;
    cin.ignore();

    nhapDanhSach(ds, n);
    cout << "\n=== DANH SACH HANG HOA VUA NHAP ===";
    xuatDanhSach(ds);

    selectionSort(ds);
    cout << "\n=== DANH SACH SAU KHI SAP XEP TANG DAN THEO GIA ===";
    xuatDanhSach(ds);

    float X;
    cout << "\nNhap gia xuat X can tim (trieu dong): ";
    cin >> X;
    timKiemNhiPhan(ds, X);

    return 0;
}
